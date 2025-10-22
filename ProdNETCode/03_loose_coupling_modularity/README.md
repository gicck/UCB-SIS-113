# Acoplamiento Débil y Modularidad

## Principio
**Acoplamiento (Coupling)**: El grado de interdependencia entre módulos
**Modularidad**: Dividir un sistema en componentes separados e intercambiables

**Acoplamiento débil** significa que los componentes dependen de abstracciones, no de implementaciones concretas.

## Ejemplo Malo (BadNotification.cs)
- Acoplamiento fuerte entre OrderProcessor y EmailSender
- Dependencias codificadas directamente (hard-coded)
- Difícil de probar o intercambiar implementaciones
- Los cambios se propagan por todo el sistema

## Ejemplo Bueno (GoodNotification/)
- Los componentes dependen de interfaces (abstracciones)
- Las dependencias se inyectan desde el exterior
- Fácil de probar con mocks
- Se pueden intercambiar implementaciones sin cambiar el código
- Estructura modular con límites claros

## Demostración Rápida
```bash
dotnet run --project BadNotification
dotnet run --project GoodNotification
```

