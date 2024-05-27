<p align="center">
  <img src="https://www.fiap.com.br/wp-content/themes/fiap2016/images/sharing/fiap.png" alt="FIAP Logo" width="150">
</p>

# Teste Técnico FIAP

Este projeto é uma aplicação web desenvolvida como parte do teste técnico da FIAP. A aplicação foi construída utilizando o framework .NET 8 e tem como objetivo gerenciar a relação entre alunos e turmas. A aplicação permite adicionar, editar, visualizar e inativar as relações entre alunos e turmas, além de fornecer uma interface de usuário para gerenciar essas entidades.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

- **Controllers**: Contém as classes que controlam o fluxo da aplicação.
- **Views**: Contém os arquivos de interface do usuário.
- **Business**: Contém a lógica de negócio da aplicação.
- **Entities**: Contém as classes que representam os dados do domínio.
- **wwwroot**: Contém arquivos estáticos como JavaScript, CSS, e imagens.

## Tecnologias Utilizadas

- **.NET 8**: Framework principal para o desenvolvimento da aplicação.
- **ASP.NET Core MVC**: Para a construção de interfaces web baseadas em modelo-visão-controlador.
- **Entity Framework Core**: Para a comunicação com o banco de dados.
- **Dapper**: Para mapeamento objeto-relacional.
- **Bootstrap**: Para o design responsivo da interface.
- **jQuery**: Para manipulação de elementos HTML e AJAX.
- **AG Grid**: Para a exibição de dados em formato de tabela.
- **Jquery AutoComplete**: Trazer dados em forma de lista, e auto complete, ou seja, em alguns campos, você escrevendo a 1 letra do aluno, da turma, deverá ser mostrado na lista


## Funcionalidades

A aplicação possui as seguintes funcionalidades:

- **Tela de Aluno**:
  - Formulário de cadastro e edição de Aluno.
  - Lista de Alunos.
  - Inativar Aluno.

- **Tela de Turma**:
  - Formulário de cadastro e edição de Turma.
  - Lista de Turmas.
  - Inativar Turma.

- **Tela de Relacionar Turmas**:
  - Formulário de Associação com dois Combos Aluno e Turma (cadastro e edição).
  - Lista de Turmas onde é possível acessar Alunos relacionados.
  - Inativar Relação.

## Regras de Negócio

- O sistema não permite Turmas com o mesmo nome.
- O sistema não permite o mesmo Aluno relacionado na mesma Turma duas vezes.
- O sistema não permite cadastrar senhas fracas.
- A senha é salva em formato de hash.

## Como Executar o Projeto

### Opção 1: Restaurar o Banco de Dados a partir de um Backup (.bak)

1. **Baixe o arquivo de backup do banco de dados**: [Backup do Banco de Dados (.bak)](https://drive.google.com/file/d/19vLYz2VBxLJU4jtAqyFiA-XCVg1LtkRi/view?usp=sharing)

2. **Abra o SQL Server Management Studio (SSMS)** e conecte-se ao seu servidor.

3. **Crie um novo banco de dados**:
    ```sql
    CREATE DATABASE TesteTecnicoFIAPDb;
    ```

4. **Restaurar o banco de dados a partir do backup**:
    - Clique com o botão direito no banco de dados recém-criado.
    - Vá até **Tarefas** e clique em **Restore** > **Database...**.
    - Escolha a opção **Device** e selecione o arquivo .bak baixado.
    - Confirme e inicie o processo de restauração.

5. **Atualize a string de conexão no arquivo `appsettings.json`** com as configurações do seu banco de dados:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=SeuServidor;Database=TesteTecnicoFIAPDb;Trusted_Connection=True;Encrypt=False;"
    }
    ```

### Opção 2: Usando Migrações do Entity Framework

1. **Atualize a string de conexão no arquivo `appsettings.json`** com as configurações do seu banco de dados:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=SeuServidor;Database=TesteTecnicoFIAPDb;Trusted_Connection=True;Encrypt=False;"
    }
    ```

2. **Restaurar os pacotes NuGet**:
    ```bash
    dotnet restore
    ```

3. **Execute as migrações do Entity Framework para criar o banco de dados**:
    ```bash
    //bash
    dotnet ef database update
    ```

4. **Execute o projeto**:
    ```bash
    dotnet run
    ```

5. **Abra o navegador e acesse**:
    ```
    http://localhost:5000
    ```

### Opção 3: Criar e Popular o Banco de Dados Manualmente

1. **Abra o SQL Server Management Studio (SSMS)** e conecte-se ao seu servidor.

2. **Crie um novo banco de dados**:
    ```sql
    CREATE DATABASE TesteTecnicoFIAPDb;
    ```

3. **Crie as tabelas necessárias**:
    ```sql
    -- Tabela de Alunos
    CREATE TABLE Alunos (
        Id INT PRIMARY KEY IDENTITY,
        Nome NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        SenhaHash VARBINARY(64) NOT NULL,
        Ativo BIT NOT NULL
    );

    -- Tabela de Turmas
    CREATE TABLE Turmas (
        Id INT PRIMARY KEY IDENTITY,
        Nome NVARCHAR(100) NOT NULL,
        Ativo BIT NOT NULL
    );

    -- Tabela de Relacionamento Aluno-Turma
    CREATE TABLE AlunoTurmas (
        AlunoId INT,
        TurmaId INT,
        PRIMARY KEY (AlunoId, TurmaId),
        FOREIGN KEY (AlunoId) REFERENCES Alunos(Id),
        FOREIGN KEY (TurmaId) REFERENCES Turmas(Id)
    );
    ```

4. **Popule as tabelas com dados iniciais**:
    ```sql
    -- Inserir dados de exemplo em Alunos
    INSERT INTO Alunos (Nome, Email, SenhaHash, Ativo) VALUES
    ('João Silva', 'joao.silva@example.com', HASHBYTES('SHA2_256', 'Senha123!'), 1),
    ('Maria Oliveira', 'maria.oliveira@example.com', HASHBYTES('SHA2_256', 'Senha123!'), 1);

    -- Inserir dados de exemplo em Turmas
    INSERT INTO Turmas (Nome, Ativo) VALUES
    ('Turma A', 1),
    ('Turma B', 1);

    -- Inserir dados de exemplo em AlunoTurmas
    INSERT INTO AlunoTurmas (AlunoId, TurmaId) VALUES
    (1, 1),
    (2, 2);
    ```

5. **Atualize a string de conexão no arquivo `appsettings.json`** com as configurações do seu banco de dados:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=SeuServidor;Database=TesteTecnicoFIAPDb;Trusted_Connection=True;Encrypt=False;"
    }
    ```



### Testes Unitários

O projeto inclui uma suíte de testes unitários para garantir a funcionalidade e a integridade do código. Os testes estão localizados no projeto `TesteTecnicoFIAP.Tests`.

Para executar os testes unitários, use o seguinte comando:
```bash
dotnet test
```

## Observações

Como citado acima, estamos usando jquery auto complete, então peço que quando escreverem por exemplo João(em tese deverá ser mostrado os Joãos, ou quem tiver nome de Joao no meio, caso não apareça nada e existe na base de dados, de um Cntrl + Backspace

## Contato

Para mais informações, entre em contato através do email:

- **Email**: [guilhermeduarte14511@gmail.com](mailto:guilhermeduarte14511@gmail.com)
