# Defensibilidad

## Principio
**Defensibilidad**: Escribir código que falle de forma segura y detecte errores tempranamente.

Conceptos clave:
- **Fail-fast**: Detectar y reportar errores inmediatamente
- **Least privilege**: Dar los permisos mínimos necesarios
- **Safe defaults**: Elegir valores predeterminados seguros y conservadores

## Ejemplo Malo (BadPayment.cs)
- Fallos silenciosos
- Valores predeterminados inseguros
- Sin validación
- Errores descubiertos tarde
- Permisos excesivos

## Ejemplo Bueno (GoodPayment.cs)
- Valida entrada tempranamente
- Falla rápido con errores claros
- Valores predeterminados seguros
- Manejo adecuado de errores
- Permisos mínimos

## Demostración Rápida
```bash
dotnet run --project BadPayment
dotnet run --project GoodPayment
```

## Principios de Defensibilidad:
- **Fail-Fast**: Mejor fallar rápido con un error claro que fallar tarde con datos corruptos
- **Least Privilege**: Solo almacenar/mostrar la información mínima necesaria
- **Safe Defaults**: Los valores predeterminados deben ser los más seguros
- **Input Validation**: Validar toda entrada antes de procesarla
- **No Silent Failures**: Siempre propagar errores de forma clara
