# Build stage
FROM node:lts-alpine as builder
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build 

# Serve Application
FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
EXPOSE 80
CMD [ "nginx", "-g", "daemon off;" ]