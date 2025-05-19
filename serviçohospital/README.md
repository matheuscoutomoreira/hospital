# 🏥 Sistema de Gestão Hospitalar e de Serviços de Saúde (SGHSS)

Este projeto é um sistema de gestão hospitalar desenvolvido como parte do Projeto Multidisciplinar 1. Ele permite gerenciar pacientes, profissionais de saúde, consultas e a administração hospitalar, com foco em segurança de dados e autenticação de acesso.

---

### 📌 Funcionalidades

- ✅ Cadastro e login de usuários com autenticação JWT  
- ✅ Controle de pacientes e profissionais de saúde  
- ✅ Histórico de consultas e prescrições  
- ✅ Criptografia de dados sensíveis com AES  
- ✅ Controle de acesso por tipo de usuário (Paciente, Profissional, Administrador)  
- ✅ Documentação interativa via Swagger  

---

### 🛠️ Tecnologias Utilizadas

- C# / ASP.NET Core  
- Entity Framework Core  
- Pomelo.EntityFrameworkCore.MySql  
- MySQL  
- JWT (Json Web Tokens)  
- AES (Advanced Encryption Standard)  
- Swagger  

---

### 🚀 Como rodar o projeto

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/sghss-api.git
cd sghss-api
```

2. Configure a conexão com o banco de dados MySQL no `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=sghss;user=root;password=sua_senha"
}
```

3. Execute as migrations:
```bash
dotnet ef database update
```

4. Inicie o servidor:
```bash
dotnet run
```

Acesse o Swagger para testar os endpoints:  
🔗 `https://localhost:5001/swagger`

---

### 🔐 Exemplo de Cadastro com Criptografia

```json
{
  "nome": "Matheus",
  "email": "matheus@gmail.com",
  "senha": "123456",
  "tipo": "Paciente"
}
```

---

### 🧾 Tipos de Usuário

| Tipo           | Valor             |
|----------------|-------------------|
| Paciente       | 0 ou `"Paciente"` |
| Profissional   | 1 ou `"Profissional"` |
| Administrador  | 2 ou `"Administrador"` |

---

### 🔑 Autenticação

Para realizar o login:

```json
{
  "email": "matheus@gmail.com",
  "senha": "123456"
}
```

Se as credenciais estiverem corretas, o sistema retorna um **token JWT**. Esse token permite acesso aos endpoints conforme o tipo de usuário.

#### ✅ Como usar o token no Swagger:

1. Copie o token retornado (sem aspas).  
2. No canto superior direito do Swagger, clique em **"Authorize"** (botão verde).  
3. Cole o token no campo, precedido da palavra `Bearer` (com espaço). Exemplo:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

4. Clique em **"Authorize"** novamente para confirmar.

Agora, os endpoints estarão autenticados de acordo com o seu tipo de usuário (Paciente, Profissional ou Administrador).

---

### 🚪 Logout

Para deslogar, basta clicar novamente no botão **"Authorize"** e depois em **"Logout"**.