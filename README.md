# TechnicalInterviewEduardo
Prueba Tecnica Multimoney Eduard Cabrera Sandi

## :hammer: Ajustes en la arquitectura
- 'WebAPI/Dtos': Se crea con con el fin de separar los DTO que exponen/reciben informacion del/al api, de los DTO de la capa de aplicacion, de esta forma se logra un mejor desacople enntre capas, de esta forma los DTO de aplicacion no los exponemos al api, 
por ejemplo en mi caso los SP que realizan tranformaciones retornan informacion sobre la transaccion, los DTO de aplicacion sirven para capturar esa informacion y realizar validaciones en en handler y en el api se retorna el DTO porpio de su capa sin exporner el DTO de aplicacion
adicinalmente me da la posibilidad de aplicar validaciones con Fluent

