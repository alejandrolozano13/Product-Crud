# 🛒 E-Commerce | CRUD de Produtos

Sistema completo de **cadastro de produtos** para o módulo administrativo de um e-commerce, dividido em dois microsserviços: **API RESTful em .NET Core** e **frontend Angular**, com persistência em **PostgreSQL**. Todo o ambiente é orquestrado com **Docker Compose**.

---

## ✅ Funcionalidades

- 🔍 Listagem de produtos
- ➕ Cadastro via formulário em modal
- ✏️ Edição inline com pré-preenchimento
- 🗑️ Exclusão lógica (flag `removed`)
- 📦 Integração com API RESTful
- 🧾 Documentação completa com Swagger
- 💬 Feedbacks visuais para erros e validações
- 🧠 Layout responsivo e direto ao ponto

---

## 📐 Estrutura do Projeto

ecommerce-crud/
├── api/ # API .NET Core
├── web/ # Frontend Angular
├── db/ # Dados e volume do PostgreSQL
├── docker-compose.yml # Orquestração dos serviços
└── README.md # Este documento
---

## 🧰 Tecnologias Utilizadas

| Camada        | Tecnologias                |
|---------------|----------------------------|
| Backend       | .NET Core 5.0, Dapper, Swagger |
| Frontend      | Angular 15+, Bootstrap 5   |
| Banco de Dados| PostgreSQL                 |
| Orquestração  | Docker + Docker Compose    |
| Validação     | FluentValidation           |

---

## ⚙️ Requisitos

- ✅ Docker instalado ([download aqui](https://www.docker.com/products/docker-desktop))
- ✅ Navegador moderno (Chrome, Edge, etc.)
- ❌ **Não precisa instalar nada manualmente** — tudo roda via Docker

---

## 🚀 Como Executar o Projeto

1. **Clone ou baixe o repositório**  
   Se preferir, baixe como `.zip` e extraia:
   ```bash
   git clone https://github.com/seu-usuario/ecommerce-crud.git

2. **Suba os containers com Docker Compose**
   docker compose up -d

3. **Acesse o sistema:**
 🧭 Frontend Angular: http://localhost:4200 | 📚 Swagger API: http://localhost:5000/swagger

---

## 🗃️ Campos do Produto

Campo	            Tipo  	      Descrição
id	              UUID    	    Gerado automaticamente pela API
code	            string      	Código visível para o usuário
description	      string	      Nome do produto
departmentCode	  string	      Código do departamento
price	            decimal	      Valor formatado em R$
active	          boolean	      Define se o produto está ativo ou não
removed	          boolean	      Define se o produto foi removido (lógico)

---

## 🗂️ Departamentos (fixos)
Esses dados são retornados por GET /api/departamentos:

Código	  Descrição
010	      Bebidas
020	      Congelados
030	      Laticínios
040	      Vegetais

---

## 🧱 Arquitetura Backend
O projeto segue uma arquitetura em camadas, respeitando os princípios de Clean Code e responsabilidade única:

bash
Copiar
Editar
api/
├── Controllers/      # Exposição de endpoints
├── Application/      # Serviços e validações (FluentValidation)
├── Domain/           # Interfaces e modelos
├── Infra/            # Repositórios com SQL puro
└── Program.cs        # Configurações e DI
Sem uso de Entity Framework

Acesso a dados com SQL explícito (Dapper)

Injeção de dependência entre camadas com IoC nativo

---

## 🧑‍💻 Frontend Angular
O projeto web foi desenvolvido com Angular 15+, incluindo:

Estrutura modularizada

Estilização com Bootstrap 5

Formulário via [(ngModel)] em modal

Filtro e formatação via pipes (currency, booleans)

Feedback de erro com *ngIf
