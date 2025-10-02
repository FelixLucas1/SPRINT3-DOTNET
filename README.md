# Nome do Projeto

Sistema de Gerenciamento de Pedidos - 3º Sprint

##  Integrantes do Grupo
rm97677 - Lucas Felix VASSILIADES

rm556588 - Gabriel Yuji Suzuki 

##  Justificativa da Arquitetura

Nesta seção, explique as decisões de arquitetura tomadas para o projeto. Abaixo um exemplo do que pode ser incluso:

*   **Arquitetura em Camadas (Onion Architecture/Clean Architecture):** Adotamos uma arquitetura em camadas para separar claramente as responsabilidades (Controller, Service, Repository, Domain), promovendo alta coesão, baixo acoplamento e facilitando testes unitários.
*   **Entity Framework Core & Oracle:** Utilizamos o EF Core como ORM para mapeamento objeto-relacional e abstração do banco de dados, configurado para se comunicar com um banco Oracle.
*   **Injeção de Dependência:** O padrão de injeção de dependência é usado extensivamente para gerenciar as dependências entre as classes, tornando o código mais modular e testável.
*   **API RESTful:** A API segue os princípios REST, utilizando corretamente os verbos HTTP (GET, POST, PUT, DELETE) e códigos de status para representar os recursos (Pedidos, Produtos, Usuários, etc.):cite[6].

##  Instruções de Execução da API

Siga os passos abaixo para executar a aplicação localmente.

### Pré-requisitos
*   **.NET 8.0 SDK** (ou a versão utilizada no projeto) instalada.
*   Um banco de dados **Oracle** configurado e acessível.
*   (Opcional) Git para clonar o repositório.

### Passos para Execução
1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/seu-usuario/nome-do-repositorio.git
    cd nome-do-repositorio
    ```

2.  **Configure a String de Conexão:**
    *   No arquivo `appsettings.json`, atualize a `DefaultConnection` dentro da seção `ConnectionStrings` com as credenciais do seu banco de dados Oracle.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SID=ORCL)));"
    }
    ```

3.  **Restaurar Pacotes e Executar Migrações (se aplicável):**
    *   Restaure os pacotes NuGet:
        ```bash
        dotnet restore
        ```
    *   Crie e execute as migrações do Entity Framework para gerar as tabelas no banco:
        ```bash
        dotnet ef migrations add InitialCreate
        dotnet ef database update
        ```

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```
    *   A API estará acessível em `https://localhost:7152` (a URL pode variar, verifique o terminal).

** Dica:** Você pode usar ferramentas como **Postman**, **Insomnia** ou o **Swagger** (se configurado na sua API) para testar esses endpoints.

##  Comando para Rodar os Testes

Para executar a suíte de testes unitários do projeto, utilize o seguinte comando no terminal, na raiz do projeto:

```bash
dotnet test
