# OpenAI Assistants API – .NET

Este projeto é uma API em **ASP.NET Core** que integra com a **Assistants API da OpenAI**, permitindo criar Assistants, iniciar conversas e trocar mensagens de forma simples e organizada.

O objetivo é oferecer uma base fácil de manter e de evoluir para interfaces gráficas, mantendo alinhamento com o padrão oficial da OpenAI.

---

## Visão Geral

A Assistants API é organizada em três conceitos principais:

* **Assistant**: define o comportamento e as instruções do modelo
* **Thread**: representa uma sessão de conversa
* **Message**: representa cada mensagem enviada ou recebida dentro de uma conversa

Esses elementos trabalham juntos para permitir conversas persistentes e contextualizadas.

---

### Configuração da OpenAI API Key

Para que a API funcione corretamente, é **obrigatório** configurar a chave da OpenAI.

Crie um arquivo `.env` na raiz do projeto e adicione a variável abaixo:

```env
	OPENAI_API_KEY=sua-chave-aqui
```

A API Key pode ser criada no painel oficial da OpenAI:  
https://platform.openai.com/account/api-keys

**Importante:**  
- Nunca versionar a chave no repositório  
- Utilize variáveis de ambiente

---

## Assistant

O Assistant é responsável por definir:

* qual modelo será utilizado
* quais instruções o modelo deve seguir
* o propósito da conversa

A existência de Assistants permite que um mesmo sistema utilize múltiplos comportamentos, como suporte técnico, atendimento comercial ou assistente interno.

Documentação oficial:
[https://platform.openai.com/docs/api-reference/assistants](https://platform.openai.com/docs/api-reference/assistants)

---

## Thread

A Thread representa uma conversa isolada.
Cada Thread mantém seu próprio histórico de mensagens, evitando mistura de contexto entre usuários ou sessões diferentes.

Uma nova Thread deve ser criada sempre que uma nova conversa for iniciada.

Documentação oficial:
[https://platform.openai.com/docs/api-reference/threads](https://platform.openai.com/docs/api-reference/threads)

---

## Message

As Messages são as mensagens enviadas dentro de uma Thread.
Elas formam o histórico da conversa e são utilizadas pelo modelo para gerar respostas mais coerentes.

As mensagens podem representar entradas do usuário ou respostas do Assistant.

Documentação oficial:
[https://platform.openai.com/docs/api-reference/messages](https://platform.openai.com/docs/api-reference/messages)

---

## Controllers

## AssistantsController

Os Assistants representam a configuração da IA.  
Cada Assistant pode ter um propósito diferente, como suporte técnico, atendimento comercial ou assistente interno.

**Documentação oficial:**  
https://platform.openai.com/docs/api-reference/assistants

### Endpoints

#### POST /api/assistants
Cria um novo Assistant.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/assistants/createAssistant

#### GET /api/assistants
Lista todos os Assistants da conta.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/assistants/listAssistants

#### GET /api/assistants/{id}
Obtém os detalhes de um Assistant específico.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/assistants/getAssistant

#### PUT /api/assistants/{id}
Atualiza informações de um Assistant existente.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/assistants/modifyAssistant

#### DELETE /api/assistants/{id}
Remove um Assistant.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/assistants/deleteAssistant

---

## ThreadsController

As Threads representam sessões de conversa independentes entre o usuário e o Assistant.

**Documentação oficial:**  
https://platform.openai.com/docs/api-reference/threads

### Endpoints

#### POST /api/threads
Cria uma nova Thread (inicia uma conversa).  

**Documentação:**  
https://platform.openai.com/docs/api-reference/threads/createThread

#### GET /api/threads/{id}
Obtém informações de uma Thread específica.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/threads/getThread

---

## MessagesController

As Messages representam as mensagens trocadas dentro de uma Thread e formam o histórico da conversa utilizado pelo Assistant.

**Documentação oficial:**  
https://platform.openai.com/docs/api-reference/messages

### Endpoints

#### POST /api/threads/{threadId}/messages
Envia uma nova mensagem para a Thread.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/messages/createMessage

#### GET /api/threads/{threadId}/messages
Lista todas as mensagens de uma Thread.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/messages/listMessages

---

## RunsController

Os Runs representam execuções do modelo em uma Thread. Cada Run corresponde a uma tentativa de processar uma resposta do Assistant para a Thread.

**Documentação oficial:**  
https://platform.openai.com/docs/api-reference/runs

### Endpoints

#### POST /api/threads/{threadId}/runs
Cria um novo Run em uma Thread, processando a próxima resposta do Assistant.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/runs/createRun

#### GET /api/threads/{threadId}/runs/{runId}
Obtém informações de um Run específico dentro de uma Thread, incluindo status, erro ou resultado.  

**Documentação:**  
https://platform.openai.com/docs/api-reference/runs/getRun

---

## Fluxo Básico de Uso

1. Criar um Assistant com as instruções desejadas  
2. Criar uma Thread para iniciar uma conversa  
3. Enviar mensagens para a Thread  
4. Criar Runs para processar respostas do Assistant

## Observações Importantes

* A Assistants API está em beta e pode sofrer alterações
* O header `OpenAI-Beta: assistants=v2` é obrigatório
* A API Key deve ser armazenada de forma segura

---

## Links Úteis

* Assistants Overview
  [https://platform.openai.com/docs/assistants](https://platform.openai.com/docs/assistants)

* API Reference
  [https://platform.openai.com/docs/api-reference](https://platform.openai.com/docs/api-reference)

* OpenAI Platform
  [https://platform.openai.com](https://platform.openai.com)

---

## Evoluções Futuras (Opcional)

* Execução de Assistants (Runs)
* Respostas em tempo real (streaming)
* Integração com front-end
* Controle de usuários e permissões
