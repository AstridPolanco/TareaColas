# TareaColas
# Sistema de Colas Multi-Proveedor en C#

Sistema modular para trabajar con diferentes proveedores de colas de mensajes implementando principios de POO y diseño flexible.

## Características Principales
- Soporte para múltiples proveedores:
  -  ActiveMQ
  -  RabbitMQ
  -  Azure Queue Storage
- Implementación basada en interfaces
- Cambio de proveedor mediante configuración
- Ejemplo funcional de producer/consumer

## Requisitos Previos
- Docker (para proveedores locales)
- Cuenta de Azure (opcional para Azure Queue Storage)

  
## Para proveedores locales
# ActiveMQ
docker run -d --name activemq -p clave -p clave

# RabbitMQ
docker run -d --name rabbitmq -p clave -p clave

## Ejecución
dotnet run --project QueueSystem.ConsoleApp

## Cambiar entre Proveedores
- Modificar QueueProvider en appsettings.json
- Asegurar servicio correspondiente en ejecución
- Volver a ejecutar la aplicación
- Ejemplo para usar RabbitMQ:
   "QueueProvider": "RabbitMQ" // Opciones: ActiveMQ, RabbitMQ, AzureQueueStorage

