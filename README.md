# TechnicalInterviewEduardo
Prueba Tecnica Multimoney Eduard Cabrera

## Adiciones al a arquitectura
** \Core\Application\Dtos\Request**
** \Core\Application\Dtos\Response**
-La intencion es segregar aun mas el uso de cada Dto y manter orden


** \Infrastructure\SpResults\**

Esta carpeta contiene objetos que representan los resultados de los Stored Procedures (SP) que ejecutan comandos

- Cada clase encapsula los parámetros de salida de un SP específico.  
- No son entidades de dominio ni DTOs de presentación.  
- Su proposito es transportar datos crudos desde la capa de infraestructura hacia la capa de Application
