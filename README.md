# Academia.Api

API para gestão de academias, desenvolvida em .NET 8, com autenticação JWT, Entity Framework Core e SQL Server.

## Sumário
- [Descrição](#descrição)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Como Executar](#como-executar)
- [Endpoints Principais](#endpoints-principais)
- [Autenticação](#autenticação)
- [Exemplo de Uso](#exemplo-de-uso)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Descrição
Esta API permite o gerenciamento de alunos, planos, presenças e usuários de uma academia. Possui autenticação baseada em JWT e segue boas práticas de arquitetura, separando domínio, infraestrutura e camada de apresentação.

## Funcionalidades
- Cadastro, consulta, atualização e remoção de alunos
- Gerenciamento de planos de assinatura
- Controle de presenças
- Cadastro e autenticação de usuários
- Controle de permissões e papéis de acesso (Admin, Gerente, Recepcionista, Instrutor, Aluno)
- Documentação automática com Swagger
- Testes automatizados (xUnit, Moq, FluentAssertions)

## Tecnologias Utilizadas
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT (JSON Web Token)
- Swagger (OpenAPI)

## Configuração do Ambiente
1. **Pré-requisitos:**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
   - (Opcional) [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/)

2. **Clonar o repositório:**
   ```bash
   git clone <url-do-repositorio>
   cd CTRustico.API
   ```

3. **Configurar a string de conexão:**
    - Por segurança, **NÃO** coloque senhas reais no arquivo versionado `appsettings.json`.
    - Recomenda-se usar variáveis de ambiente para a connection string em produção:
       - No Windows:
          ```powershell
          $env:DefaultConnection="Server=localhost,1433;Database=CTRustico;User Id=ctrustico;Password=SuaSenhaAqui;TrustServerCertificate=True;"
          ```
       - No Linux/macOS:
          ```bash
          export DefaultConnection="Server=localhost,1433;Database=CTRustico;User Id=ctrustico;Password=SuaSenhaAqui;TrustServerCertificate=True;"
          ```
    - No arquivo `appsettings.Development.json`, use um placeholder ou deixe a senha em branco para evitar exposição:
       ```json
       "DefaultConnection": "Server=localhost,1433;Database=CTRustico;User Id=ctrustico;Password=__SENHA_AQUI__;TrustServerCertificate=True;"
       ```
    - O valor da variável de ambiente sempre sobrescreve o do arquivo.

4. **Configurar as chaves JWT:**
    - No mesmo arquivo, configure a seção `Jwt`:
       ```json
       "Jwt": {
          "Key": "sua-chave-secreta",
          "Issuer": "sua-empresa",
          "Audience": "sua-audiencia",
          "ExpireHours": "2"
       }
       ```

5. **Criar tabelas e permissões iniciais:**
    - Execute os scripts em `Academia.Infrastructure/Data/Scripts/` no seu banco de dados:
       - `CreatePermissaoTables.sql` — Cria as tabelas de permissões e relacionamento
       - `InsertPermissoes.sql` — Popula permissões padrão

## Como Executar
1. **Restaurar pacotes:**
   ```bash
   dotnet restore Academia.Api/Academia.Api.csproj
   ```
2. **Aplicar as migrations (se houver):**
   ```bash
   dotnet ef database update --project Academia.Infrastructure/Academia.Infrastructure.csproj
   ```
3. **Executar a API:**
   ```bash
   dotnet run --project Academia.Api/Academia.Api.csproj
   ```
4. **Acessar a documentação:**
   - Acesse `https://localhost:5001/swagger` no navegador.

## Endpoints Principais
- `POST /api/auth/login` — Autenticação de usuário (retorna JWT)
- `GET /api/alunos` — Listar alunos
- `POST /api/alunos` — Cadastrar aluno
- `PUT /api/alunos/{id}` — Atualizar aluno
- `DELETE /api/alunos/{id}` — Remover aluno
- `GET /api/planos` — Listar planos
- `POST /api/presencas` — Registrar presença
- `POST /api/usuarios` — Cadastro de usuário (com permissões)

> Consulte o Swagger para todos os endpoints, exemplos de payload e detalhes de DTOs externos.

## Autenticação
- Utilize o endpoint `/api/auth/login` para obter um token JWT.
- Envie o token no header `Authorization: Bearer {token}` para acessar endpoints protegidos.

## Exemplos de Uso

### Login
```http
POST /api/auth/login
Content-Type: application/json

{
   "email": "admin@email.com",
   "password": "senha"
}
```
Resposta:
```json
{
   "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

### Cadastro de Usuário com Permissões
```http
POST /api/usuarios
Content-Type: application/json

{
   "nome": "Novo Usuário",
   "email": "novo@email.com",
   "password": "senha123",
   "perfil": "Admin",
   "permissoesIds": [1, 2]
}
```

## Estrutura do Projeto
```
Academia.Api/            # Camada de apresentação (controllers, Program.cs, configs, DTOs)
Academia.Domain/         # Entidades e regras de negócio
Academia.Infrastructure/ # Persistência de dados (DbContext, Migrations, Scripts SQL)
Academia.Tests/          # Testes unitários automatizados
```

## Testes Automatizados
O projeto possui cobertura de testes para controllers e services principais, utilizando xUnit, Moq e FluentAssertions. Para rodar os testes:

```bash
dotnet test Academia.Tests/Academia.Tests.csproj
```

## Contribuição
Pull requests são bem-vindos! Para grandes mudanças, abra uma issue primeiro para discutir o que você gostaria de modificar.

## Licença
Este projeto está sob a licença MIT.
