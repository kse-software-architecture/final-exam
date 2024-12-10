# Final Exam

---

## Overview

Your team is planning to deliver a new product: a specialized AI assistant for IT support. Since an LLM is currently under active development, you’ve decided to begin by building a service MVP using ChatGPT, with plans to replace it later. The proof-of-concept code has been approved, and you now need to refactor it to move forward.

---

## Task

You have a web service where a single ticket with question can be created, and answer is fetched from chatGPT. You need to take this code (or rewrite it from scratch if you want to) to move this project to Clean Architectural style and:
1. Add possibility to open new tickets with new questions;
2. Give responses to specific tickets continuing dialogs there. Every user request (question) should be followed by an answer from AI;
3. View chat history and other information for specific ticket. In future we're planning to store it in DynamoDB but now it is ok to store it just in runtime and to lose it during server reboots;
4. Implement a possibility to limit amount of tickets per user - [429 Payment Required](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/402) status should be returned if user is trying to create more than 5 tickets. In future more limits based on users subscriptions plans will be introduced.

## Details
1. In order to keep context in conversation with AI _all previous messages_ should be given as input to a model.
2. Http API is negotiated and approved by web team, so you can not change it. Keep those provided three endpoints with proposed contracts.
3. We're skipping authorization and authentication part to make this project simpler, so you can just assume that `UserId` provided in requests body is always correct and validated.

### Grading Policy

**2 points** - following overall Clean Architecture Guidelines

**2 points** - implementing a tickets logic

**2 points** - following SOLID principles


Good luck with your implementation!