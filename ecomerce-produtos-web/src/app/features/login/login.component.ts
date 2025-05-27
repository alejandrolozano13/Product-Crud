import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formularioLogin!: FormGroup;
  erroLogin: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) { }

  ngOnInit() {
    this.formularioLogin = this.fb.group({
      usuario: ['', Validators.required],
      senha: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.formularioLogin.invalid) {
      this.erroLogin = 'Por favor, preencha usuário e senha';
      return;
    }

    this.erroLogin = '';

    const dadosLogin = {
      UserAPI: this.formularioLogin.get('usuario')?.value,
      Password: this.formularioLogin.get('senha')?.value
    };

    this.http.post<{ token: string }>('http://localhost:5000/Auth/login', dadosLogin)
      .subscribe({
        next: (resposta) => {
          localStorage.setItem('token', resposta.token);
          this.router.navigate(['/products']);
        },
        error: (erro) => {
          this.erroLogin = 'Usuário ou senha inválidos';
          console.error('Erro no login:', erro);
        }
      });
  }
}