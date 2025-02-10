```markdown
# B3 Simulador de investimento

## 🛠️ Tecnologias Utilizadas
- .NET Core 8.0  
- xUnit  
- Docker  
- OpenAPI  
- Angular
- EsLint
---

## 🚀 Como Executar o Projeto

### **Usando Docker**
1. Navegue até o diretório `\src`.  
2. Execute o comando:
   bash
   docker-compose up
   Isso irá iniciar a aplicação frontend e do server, eles serão servidos em:
   Server: http://localhost:8080 (para swagger /swagger/index.html)
   Interface angular: http://localhost:4200/

### **Visual Studio**
(arquivo da solução: /src/FinantialProjectBE.sln)
1. Vá até as configurações de Startup Item (ao lado esquerdo bobotão padrão de start), e selecione a configuração "ServerAndFront"
2. Pode dar start pelo visual studio que ele vai subir o server e o fronend, abrindo a interface no navegador e o server com swagger também

### **Outros**
Com Visual code ou até mesmo o console, é possível iniciar os projetos separados, sendo necessário ter instalado o dotnet e o angular CLI (ng)
```

## 🧪 Executando Testes
Os testes unitários e de integração estão disponíveis no projeto `tests`, que já está adicionado a solução principal.

---

## 📚 Uso da API
Como a API está totalmente documentada no Swagger, não há necessidade de listar todas as URLs aqui. Abaixo estão detalhes importantes sobre seu funcionamento:

### Campos e Funcionamento
- **Método getCalculation (GET):** realiza os calculos e validações dos valores enviados para o usuário.

---
