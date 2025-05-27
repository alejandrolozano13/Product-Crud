import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // <-- importante
import { Product } from '../../../domain/models/product.model';
import { ProductService } from '../../../core/services/productService';
import { DepartmentService, Department } from '../../../core/services/departmentService';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  departamentos: Department[] = [];

  isEditando: boolean = false;

  erro: string = '';

  novoProduto: Product = {
    code: '',
    description: '',
    departmentCode: '',
    price: 0,
    active: true,
    removed: false
  };

  constructor(
    private productService: ProductService,
    private departmentService: DepartmentService
  ) { }

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: (res) => this.products = res,
      error: (err) => {
        this.erro = 'Erro ao carregar produtos';
        console.error('Erro ao carregar produtos:', err);
      },
    });
  }

  carregarDepartamentos(): void {
    this.departamentos = []; // limpa para evitar dados antigos
    this.departmentService.getAll().subscribe({
      next: (res) => {
        this.departamentos = res;
      },
      error: (err) => {
        console.error('Erro ao carregar departamentos:', err);
        this.departamentos = [];
      }
    });
  }

  get podeSalvar(): boolean {
    return (
      this.novoProduto.code.trim().length > 0 &&
      this.novoProduto.description.trim().length > 0 &&
      this.novoProduto.departmentCode.trim().length > 0 &&
      this.novoProduto.price > 0
    );
  }

  formatarPreco(event: any) {
    let valor = event.target.value;

    valor = valor.replace(/[^\d,.-]/g, '');
    valor = valor.replace(',', '.');

    const valorNumerico = parseFloat(valor);

    if (!isNaN(valorNumerico) && valorNumerico > 0) {
      this.novoProduto.price = valorNumerico;
      event.target.value = valorNumerico.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
      });
    } else {
      this.novoProduto.price = 0;
      event.target.value = '';
    }
  }

  adicionarProduto(): void {
    this.novoProduto = {
      code: '',
      description: '',
      departmentCode: '',
      price: 0,
      active: true,
      removed: false
    };
    this.isEditando = false;

    this.carregarDepartamentos();

    const modalEl = document.getElementById('modalAdicionarProduto')!;
    const modal = new bootstrap.Modal(modalEl);
    modal.show();
  }

  salvarProduto(): void {
    const formEl = document.querySelector('form') as HTMLFormElement;

    if (!this.podeSalvar) {
      alert('Preencha todos os campos corretamente. Preço deve ser maior que zero.');
      return;
    }

    if (!formEl.checkValidity()) {
      Object.values(formEl.elements).forEach((el: any) => {
        if (el?.classList?.contains('form-control') || el?.classList?.contains('form-select')) {
          el.classList.add('ng-touched');
        }
      });
      return;
    }

    const request$ = this.isEditando
      ? this.productService.update(this.novoProduto)
      : this.productService.add(this.novoProduto);

    request$.subscribe({
      next: () => {
        const modalEl = document.getElementById('modalAdicionarProduto')!;
        const modal = bootstrap.Modal.getInstance(modalEl);
        modal?.hide();

        this.productService.getAll().subscribe(res => this.products = res);
      },
      error: (err) => {
        if (err.error) {
          if (typeof err.error === 'string') {
            // Caso seja uma string com a mensagem completa do erro
            this.erro = this.extrairMensagemErro(err.error);
          } else if (err.error.message) {
            // Caso seja objeto com campo message
            this.erro = err.error.message;
          } else {
            this.erro = 'Erro desconhecido ao salvar produto.';
          }
        } else {
          this.erro = 'Erro desconhecido ao salvar produto.';
        }
      }
    });
  }

  private extrairMensagemErro(textoErro: string): string {
    const linha = textoErro.split('\n').find(l => l.includes("System.Exception"));
    if (linha) {
      const msg = linha.split(':').slice(1).join(':').trim();
      return msg || 'Erro ao salvar produto.';
    }
    return 'Erro ao salvar produto.';
  }

  private fecharModal(): void {
    const modalEl = document.getElementById('modalAdicionarProduto')!;
    const modal = bootstrap.Modal.getInstance(modalEl);
    modal?.hide();
  }

  editarProduto(produto: Product): void {
    this.isEditando = true;
    this.novoProduto = { ...produto };

    this.carregarDepartamentos();

    const modalEl = document.getElementById('modalAdicionarProduto')!;
    const modal = new bootstrap.Modal(modalEl);
    modal.show();
  }

  removerProduto(produto: Product): void {
    const confirmacao = confirm(`Deseja realmente excluir o produto "${produto.description}"?`);
    if (!confirmacao) return;

    this.productService.delete(produto.id!).subscribe({
      next: () => {
        console.log('Produto removido com sucesso!');
        this.productService.getAll().subscribe(res => this.products = res);
      },
      error: (err) => {
        console.error('Erro ao remover produto:', err);
      }
    });
  }
}