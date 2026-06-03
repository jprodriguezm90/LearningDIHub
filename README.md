# LearningDIHub

## Overview

LearningDIHub is a playground project created to explore and understand Dependency Injection concepts in .NET.

The main goal of this repository is to practice how object creation, service resolution, and dependency lifetimes work behind the scenes while building small experiments and examples.

---

## Goals

This project was created to:

* Learn Dependency Injection fundamentals
* Understand Service Lifetimes
* Practice Service Registration and Resolution
* Explore Multiple Implementations
* Experiment with Object Composition
* Improve understanding of .NET application architecture

---

## Concepts Covered

### Dependency Injection

* Constructor Injection
* Service Registration
* Service Resolution
* Service Collections
* Service Providers

### Service Lifetimes

* Singleton
* Scoped
* Transient

### Advanced DI Concepts

* Multiple Implementations
* IEnumerable Injection
* Circular Dependencies
* Captive Dependencies
* Lifetime Validation

---

## Project Structure

```text
LearningDIHub.Presentation
│
├── Controllers
├── Program.cs

LearningDIHub.Domain
│
├── Contracts
├── Services
├── Models

LearningDIHub.Data
│
├── Persistence
├── Providers
```

---

## Experiments

Some examples implemented in this project:

* Registering multiple implementations of the same interface
* Testing Singleton vs Scoped vs Transient behavior
* Exploring circular dependency scenarios
* Understanding how ASP.NET Core resolves services
* Creating services manually using ServiceCollection
* Using Dependency Injection inside ASP.NET Core Web Applications

---

## Running the Project

Clone repository:

```bash
git clone <repository-url>
```

Run:

```bash
dotnet run
```

---

## Purpose

This repository is intended for learning, experimentation, and interview preparation rather than production usage.
