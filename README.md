# Proyecto CRM

## Primera Entrega

**Desafíos Técnicos**

- Definir las relaciones entre `Customer`, `Interaction`, `Meeting`, `Sale` y demás clases requirió aplicar correctamente guías de diseño como Expert. La asignación de responsabilidades fue iterativa, con varios ajustes hasta lograr una estructura coherente y mantenible.
- Mantener las buenas prácticas de programación mientras buscábamos soluciones fue un desafío constante. La presión de resolver problemas nos generaba el temor de incumplir algún principio `SOLID`, patrón de diseño o guía establecida. En momentos de bloqueo, había que frenar y asegurarnos de no sacrificar calidad por velocidad.
- Implementamos polimorfismo para manejar diferentes tipos de interacciones, lo que permitió extender funcionalidades sin modificar código existente y facilitó la escalabilidad del sistema.
- La lógica para identificar clientes sin actividad reciente implicó trabajar con `DateTime` y `TimeSpan`. Métodos como `GetInactiveCustomers` y `GetRecentInteraction` requirieron varias iteraciones hasta quedar robustos y reutilizables.
- Cobertura de pruebas unitarias con `NUnit`, este proceso tomó tiempo considerable pero resultó fundamental para validar comportamientos y detectar errores tempranamente, estableciendo una base sólida para el desarrollo futuro.
- El uso de `GitHub` como herramienta de gestión nos obligó a aprender manejo de ramas, resolución de conflictos y coordinación de tareas simultáneas. La curva de aprendizaje fue pronunciada pero necesaria.

**Aprendizajes Clave**
- Aplicación práctica de principios OO y su impacto directo en mantenibilidad y escalabilidad del código.
- Integración de herramientas de testing como parte esencial del flujo de desarrollo, no como paso opcional.
- Uso de GitHub no solo como repositorio sino como plataforma de coordinación y documentación del proyecto.
- Importancia de las retrospectivas para identificar puntos de mejora en planificación y comunicación interna.

**Balance del Equipo**

Llegamos a una solución final para esta primera entrega, pero como en todo proyecto, hay cosas que podemos mejorar. El punto más claro que salió en las retrospectivas fue la organización y coordinación. Si bien hubo organización, nos faltó tomarnos más tiempo al principio para definir bien las tareas y dividirlas entre todos. Pensar un poco más a futuro nos habría dado más margen para estar disponibles ante imprevistos o correcciones de último momento.
En cuanto a comunicación, somos un equipo donde todos tienen voz. Le damos rienda suelta al debate y valoramos la opinión de cada integrante, lo que genera un ambiente de trabajo colaborativo y sin trabas para expresar ideas o dudas. Eso fue un punto fuerte que queremos mantener.
Para las próximas entregas, el objetivo es claro: más tiempo en la planificación inicial, definir tareas más específicas desde el arranque, y tener una visión un poco más a largo plazo para anticiparnos a los baches del camino.<br/>
**En resumen: esta etapa puso los cimientos de algo sólido. Cada uno metió lo suyo, aprendimos un montón tanto de código como de trabajo en equipo, y el CRC está tomando forma. Ahora, a aplicar lo aprendido en la siguiente entrega.**




## Segunda Entrega

### Evaluación General

Estamos muy conformes con el desempeño alcanzado y con las devoluciones obtenidas por parte del cuerpo docente. Sentimos que esta segunda etapa reflejó una mejora clara tanto en la organización como en la calidad del código. Si bien todavía hay aspectos que podemos seguir puliendo, logramos una resolución sólida del sistema y una mayor claridad en la estructura general del proyecto.

**Avances Técnicos**

- En esta entrega incorporamos patrones de diseño que aportaron significativamente a la organización y funcionalidad del sistema.
- El patrón `Facade` se implementó para facilitar la interacción del usuario con el sistema, permitiendo que este no tenga que conocer la estructura interna del programa. A través de la fachada, los usuarios pueden ingresar los datos directamente en formato `string`, simplificando el uso y haciendo más intuitiva la comunicación con las distintas funcionalidades del sistema.
- Por otro lado, el patrón `Singleton` nos permitió mantener una única instancia de cada manager, garantizando que las listas se manejen de forma estática y global. De esta manera, al acceder a ellas desde cualquier parte del programa, siempre se trabaja con los mismos datos actualizados, evitando duplicaciones y asegurando la coherencia en la gestión de la información.
- Además, integramos `Doxygen` como herramienta de documentación, lo que nos permitió mantener una descripción clara y profesional del código, detallando clases, métodos y relaciones del sistema. Esto facilita tanto la comprensión interna entre los miembros del equipo como la futura revisión por parte de terceros.

> [!IMPORTANT]   
> Dado que `Interaction` es una clase abstracta, fue necesario crear una clase concreta llamada **`InteractionRegular`** para realizar las pruebas unitarias.  
> Esta clase hereda de la abstracta y permitió testear las funcionalidades comunes definidas en la clase base, asegurando el correcto comportamiento de la jerarquía de interacciones y la robustez del diseño orientado a objetos.

**Organización y Trabajo en Equipo**

- A nivel organizacional, logramos planificar y avanzar de manera más estructurada, superando dificultades externas y equilibrando las distintas tareas ajenas a la programación. La experiencia previa nos permitió distribuir mejor las responsabilidades, mejorar la comunicación y mantener un seguimiento más constante del progreso del proyecto.
- El equipo se mostró comprometido y colaborativo, trabajando de forma coordinada para alcanzar los objetivos establecidos. Se reforzó la autonomía de cada integrante y se mantuvo un ambiente de trabajo abierto al diálogo y al aprendizaje colectivo.

**Reflexión Final**

En esta segunda entrega pudimos ver reflejado el crecimiento del equipo tanto a nivel técnico como organizativo. La incorporación de los patrones `Singleton` y `Facade`, junto con el uso de `Doxygen`, nos permitió estructurar mejor el sistema y hacerlo más accesible para los usuarios. Aprendimos a planificar con más precisión, a documentar de manera profesional y a mantener una comunicación fluida entre todos los integrantes. De cara a la última etapa del proyecto, nuestro objetivo será integrar el sistema con Discord, aplicando todo lo aprendido hasta ahora para lograr una versión completa y funcional. Sentimos que esta entrega fue un paso importante hacia esa meta, consolidando la base de un proyecto que creció tanto en su desarrollo técnico como en el trabajo en equipo.

> [!NOTE]
> En el siguiente link se podrán encontrar con la web de la documentación:
> https://santiagocardinal.github.io/Proyecto_Bienn/html/index.html
>

 ## Entrega Final

En esta tercera y última entrega seguimos incorporando nuevas herramientas y consolidando todo lo trabajado a lo largo del proyecto. El foco principal fue integrar nuestro **CRM con Discord**, lo que nos obligó a adaptar el sistema a un entorno real de interacción y a pensar cuidadosamente cómo presentar la información de forma clara y útil para el usuario final.

**Trabajo Técnico**

Mantuvimos el uso de los patrones `Facade` (aunque le tuvimos que modificar algunos aspectos para que el programa funcione correctamente) y `Singleton`. La fachada volvió a ser clave para simplificar la interacción entre el usuario y el sistema, especialmente ahora que todo se gestiona mediante comandos de Discord. 

La integración con Discord nos llevó a trabajar exclusivamente con inputs en formato _string_, los cuales debían ser validados, divididos y convertidos en objetos internos del sistema. 

 Para lograr un funcionamiento claro y mantenible, organizamos todos los comandos de forma ordenada, asegurándonos de que cada uno cumpliera una única responsabilidad. 

 **Organización del Equipo**
 En comparación con las entregas anteriores, sentimos que esta fue la más ordenada a nivel grupal. La experiencia previa trabajando juntos se notó claramente: 

- Repartimos las tareas con más criterio, 

- La comunicación fue más fluida, 

- Cada integrante tuvo mayor autonomía sin perder la coordinación general del equipo. 

**Desafíos**

La activación del bot fue una de las etapas que más trabajo nos dio. Aunque en teoría parecía sencillo crear el bot, obtener el token y conectarlo al proyecto en la práctica fue un proceso repleto de detalles finos que ajustar. 

El primer obstáculo fue la configuración inicial del bot en el panel de Discord. A pesar de seguir la documentación oficial, muchas de las opciones de permisos estaban repartidas en distintas secciones, lo que nos obligó a revisarlo todo manualmente varias veces. Tras varias lecturas del documento de inicialización y gracias al apoyo de compañeros, finalmente logramos activar el bot sin fallas. 

Otro punto desafiante fue la interacción entre el bot y nuestra fachada. Hubo momentos en los que el bot recibía los datos correctamente, pero el parseo fallaba; esto nos llevó a revisar la forma en que dividíamos los parámetros y cómo manejábamos los distintos casos de error. 

**Reflexión Final** 

Cerramos el proyecto con la sensación de __haber crecido mucho__ desde el inicio. Pasamos por varias etapas: `diseño de clases`, `pruebas unitarias`, `documentación`, `patrones de diseño`, `manejo de GitHub` y, finalmente, `integración con un entorno real como Discord`. 

 
 


