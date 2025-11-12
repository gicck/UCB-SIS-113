# Code Review de Principios de Diseño

- **Duración:** 90 minutos- 
- **Objetivo:** Identificar y refactorizar violaciones de principios de diseño en su propio proyecto

---Checklist de Inspección

1. SRP - Responsabilidad Única

 ¿Hay clases con más de 200 líneas?
 ¿Alguna clase tiene métodos de validación, acceso a DB, y lógica de negocio juntos?
 ¿Hay nombres de clase con "Manager", "Handler", "Util", "Helper"? (señales de alerta)

2. Encapsulación

 ¿Hay campos públicos (public int Campo;)?
 ¿Usan propiedades con { get; set; } sin validación?
 ¿Exponen colecciones directamente (public List<T> Items;)?

3. Acoplamiento Débil

 ¿Hay new dentro de constructores o métodos? (Ej: new SqlConnection())
 ¿Hay hardcoding de servicios específicos?
 ¿Usan interfaces o todo es concreto?

4. Defensibilidad

 ¿Hay métodos sin validación de parámetros?
 ¿Registran contraseñas o información sensible en logs?
 ¿Usan valores por defecto seguros?

5. DRY (Don't Repeat Yourself)

 ¿Hay código copy-paste en varios lugares?
 ¿La misma lógica de validación está repetida?