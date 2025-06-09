
# 📦 API de Drones (.NET 8 + MySQL em Containers)

Este projeto é uma API RESTful desenvolvida em .NET 8 para o gerenciamento de drones. A aplicação realiza operações completas de CRUD (Create, Read, Update, Delete) e utiliza um banco de dados MySQL em container com persistência de dados.

---

## 📁 Conteúdo do Repositório

Este repositório contém tudo o que é necessário para você rodar o projeto e testar a API de Drones:

* **Código-fonte da aplicação:** O código-fonte completo da API .NET.
* **Arquivos Dockerfile:** O `Dockerfile` para a construção da imagem Docker da aplicação.
* **Scripts ou JSONs usados nos testes do CRUD:** Arquivos `.json` (e/ou scripts, se aplicável) que podem ser usados para testar as operações de CRUD da API.

---

## ✅ Requisitos Atendidos

- [x] API em .NET com CRUD completo  
- [x] Dockerfile personalizado (usuário não-root, diretório de trabalho, variáveis de ambiente)  
- [x] Aplicação exposta em uma porta configurável  
- [x] Container do MySQL com volume, variáveis de ambiente e porta exposta  
- [x] Banco de dados diferente do H2  
- [x] Testes de CRUD com arquivos JSON  

---

## ⚙️ Subindo o Ambiente com Docker (Passo a Passo)

Para colocar o ambiente no ar, siga os passos abaixo na ordem:

### 🐬 1. Subindo o Container do Banco de Dados (MySQL)

Primeiro, inicie o container do MySQL. Isso cria o banco de dados e o usuário necessários para a API.

```bash
docker run -d \
  --name mysql-container \
  -e MYSQL_ROOT_PASSWORD=root123 \
  -e MYSQL_DATABASE=meubanco \
  -e MYSQL_USER=meuusuario \
  -e MYSQL_PASSWORD=senha123 \
  -v mysql_data:/var/lib/mysql \
  -p 3306:3306 \
  mysql:8.0
```

### 🐳 2. Build da Imagem da Aplicação .NET

Em seguida, construa a imagem Docker da sua aplicação .NET. Certifique-se de estar na raiz do projeto onde o Dockerfile está localizado.

```bash
docker build -t smartdrones-api .
```

### 🚀 3. Execução do Container da Aplicação

Após o build da imagem, execute o container da sua API. Assegure-se de que a string de conexão esteja configurada corretamente para apontar para o container MySQL.

```bash
docker run -d \
  --name smartdrones-api \
  -e ConnectionStrings__DefaultConnection="Server=host.docker.internal;Port=3306;Database=meubanco;User=meuusuario;Password=senha123;" \
  -p 5000:5000 \
  smartdrones-api
```

---

### 📝 Observações Importantes

- Certifique-se de que as portas 3306 (MySQL) e 5000 (aplicação .NET) estejam disponíveis em sua máquina.  
- Não é necessário usar `docker-compose`; os containers são executados individualmente com comandos `docker run`.  
- Para verificar os logs da aplicação e depurar possíveis problemas, use o comando:

```bash
docker logs smartdrones-api
```

- Se precisar parar e remover os containers, use:

```bash
docker stop smartdrones-api mysql-container
docker rm smartdrones-api mysql-container
```
