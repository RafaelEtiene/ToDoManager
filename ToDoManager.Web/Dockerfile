# Use a imagem base do Node.js para construir a aplicação
FROM node:18 AS build

# Defina o diretório de trabalho dentro do container
WORKDIR /app

# Copie o arquivo package.json e instale as dependências
COPY package*.json ./
RUN npm install

# Copie o restante dos arquivos do projeto e construa a aplicação
COPY . ./
RUN npm run build --prod

# Use uma imagem base do Nginx para servir a aplicação estática
FROM nginx:alpine

# Copie os arquivos de build do Angular para o Nginx
# Verifique se o caminho está correto para o seu projeto
COPY --from=build /app/dist/to-do-manager.web /usr/share/nginx/html

# Copie o arquivo de configuração do Nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Exponha a porta 80 para acessar o app Angular
EXPOSE 4200

# Inicie o Nginx para servir o app
CMD ["nginx", "-g", "daemon off;"]
