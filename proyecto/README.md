# Proyecto CRM

## Primera Entrega

**Desafíos Técnicos**

- Arquitectura y asignación de responsabilidades
- Definir las relaciones entre `Customer`, `Interaction`, `Meeting`, `Sale` y demás clases requirió aplicar correctamente guías de diseño como Expert. La asignación de responsabilidades fue iterativa, con varios ajustes hasta lograr una estructura coherente y mantenible.
- Adherencia a principios y patrones de diseño
- Mantener las buenas prácticas de programación mientras buscábamos soluciones fue un desafío constante. La presión de resolver problemas nos generaba el temor de incumplir algún principio `SOLID`, patrón de diseño o guía establecida. En momentos de bloqueo, había que frenar y asegurarnos de no sacrificar calidad por velocidad.
- Polimorfismo para tipos de interacciones
- Implementamos polimorfismo para manejar diferentes tipos de interacciones, lo que permitió extender funcionalidades sin modificar código existente y facilitó la escalabilidad del sistema.
- Detección de clientes inactivos
- La lógica para identificar clientes sin actividad reciente implicó trabajar con `DateTime` y `TimeSpan`. Métodos como `GetInactiveCustomers` y `GetRecentInteraction` requirieron varias iteraciones hasta quedar robustos y reutilizables.
- Cobertura de pruebas unitarias con `NUnit`
- Implementamos tests para todas las clases y métodos del sistema. Este proceso tomó tiempo considerable pero resultó fundamental para validar comportamientos y detectar errores tempranamente, estableciendo una base sólida para el desarrollo futuro.
- Control de versiones y trabajo colaborativo
- El uso de GitHub como herramienta de gestión nos obligó a aprender manejo de ramas, resolución de conflictos y coordinación de tareas simultáneas. La curva de aprendizaje fue pronunciada pero necesaria.

**Aprendizajes Clave**
- Aplicación práctica de principios OO y su impacto directo en mantenibilidad y escalabilidad del código.
- Integración de herramientas de testing como parte esencial del flujo de desarrollo, no como paso opcional.
- Uso de GitHub no solo como repositorio sino como plataforma de coordinación y documentación del proyecto.
- Importancia de las retrospectivas para identificar puntos de mejora en planificación y comunicación interna.

**Balance del Equipo**<br/>
Llegamos a una solución final para esta primera entrega, pero como en todo proyecto, hay cosas que podemos mejorar. El punto más claro que salió en las retrospectivas fue la organización y coordinación. Si bien hubo organización, nos faltó tomarnos más tiempo al principio para definir bien las tareas y dividirlas entre todos. Pensar un poco más a futuro nos habría dado más margen para estar disponibles ante imprevistos o correcciones de último momento.
En cuanto a comunicación, somos un equipo donde todos tienen voz. Le damos rienda suelta al debate y valoramos la opinión de cada integrante, lo que genera un ambiente de trabajo colaborativo y sin trabas para expresar ideas o dudas. Eso fue un punto fuerte que queremos mantener.
Para las próximas entregas, el objetivo es claro: más tiempo en la planificación inicial, definir tareas más específicas desde el arranque, y tener una visión un poco más a largo plazo para anticiparnos a los baches del camino.<br/>
**En resumen: esta etapa puso los cimientos de algo sólido. Cada uno metió lo suyo, aprendimos un montón tanto de código como de trabajo en equipo, y el CRC está tomando forma. Ahora, a aplicar lo aprendido en la siguiente entrega.**
