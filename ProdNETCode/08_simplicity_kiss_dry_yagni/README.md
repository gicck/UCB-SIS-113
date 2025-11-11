# Simplicidad: KISS, DRY, YAGNI

## Principios

**KISS (Keep It Simple, Stupid)**: Preferir soluciones simples sobre las complejas

**DRY (Don't Repeat Yourself)**: Evitar la duplicación de código

**YAGNI (You Aren't Gonna Need It)**: No agregar funcionalidad hasta que sea necesaria

Estos principios ayudan a evitar la sobre-ingeniería.

## Ejemplo Malo (BadStringUtils.cs)
- Soluciones sobre-ingenieradas
- Código repetido
- Características innecesarias
- Abstracciones complejas para tareas simples

## Ejemplo Bueno (GoodStringUtils.cs)
- Código simple y directo
- Funciones reutilizables
- Solo características necesarias
- Fácil de entender

## Demostración Rápida
```bash
dotnet run BadStringUtils.cs
dotnet run GoodStringUtils.cs
```
