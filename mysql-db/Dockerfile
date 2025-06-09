# Dockerfile para o serviço MySQL
# Usa a imagem oficial do MySQL 8.0 como base.
FROM mysql:8.0

# EXPOSE 3306 é geralmente opcional no Dockerfile, pois o Docker Compose
# gerencia a exposição de portas. No entanto, é boa prática documentar
# as portas que o contêiner pretende usar.
EXPOSE 3306

# Os requisitos de "usuário não-root" e "diretório de trabalho" são
# implicitamente atendidos pela imagem oficial do MySQL, que já roda
# como um usuário 'mysql' e tem um diretório de trabalho padrão adequado.
# Se fosse uma imagem base diferente, seria necessário adicionar:
# RUN useradd -m meuusuario
# USER meuusuario
# WORKDIR /minha/pasta/de/trabalho

# Você pode adicionar scripts de inicialização de banco de dados aqui.
# Por exemplo, para copiar um arquivo .sql que cria o esquema:
# COPY init.sql /docker-entrypoint-initdb.d/
# (Não é necessário agora, pois você não forneceu um, mas é uma opção). 