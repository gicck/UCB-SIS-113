// Program.cs - ESTE CÓDIGO DEBE FUNCIONAR CON TU IMPLEMENTACIÓN
using System;
using System.Collections.Generic;

namespace HospitalManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE GESTIÓN HOSPITALARIA ===\n");
            
            // CASO DE PRUEBA 1: Creación y validaciones básicas
            TestCreacionYValidaciones();
            
            // CASO DE PRUEBA 2: Operaciones médicas controladas
            TestOperacionesMedicas();
            
            // CASO DE PRUEBA 3: Sistema de seguridad y auditoría
            TestSeguridadYAuditoria();
            
            // CASO DE PRUEBA 4: Escenarios de error y límites
            TestEscenariosLimite();
            
            Console.WriteLine("\n=== FIN DE PRUEBAS ===");
            Console.ReadKey();
        }

        static void TestCreacionYValidaciones()
        {
            Console.WriteLine("--- CASO 1: Creación y Validaciones ---");
            
            // Crear sistema hospitalario
            var hospital = new SistemaHospital();
            
            // Crear pacientes con validaciones
            var paciente1 = hospital.RegistrarPaciente("H001", "Juan Pérez", 35);
            var paciente2 = hospital.RegistrarPaciente("H002", "María García", 28);
            
            // Intentar crear paciente con edad inválida
            var pacienteInvalido = hospital.RegistrarPaciente("H003", "Pedro Inválido", 150);
            
            // Crear médicos
            var medico1 = hospital.RegistrarMedico("M001", "Dr. Rodriguez", "Cardiología");
            var medico2 = hospital.RegistrarMedico("M002", "Dra. López", "Pediatría");
            
            Console.WriteLine($"Pacientes registrados: {hospital.TotalPacientes}");
            Console.WriteLine($"Médicos registrados: {hospital.TotalMedicos}");
            Console.WriteLine();
        }

        static void TestOperacionesMedicas()
        {
            Console.WriteLine("--- CASO 2: Operaciones Médicas ---");
            
            var hospital = new SistemaHospital();
            
            // Crear entidades
            var paciente = hospital.RegistrarPaciente("H101", "Carlos Mendez", 45);
            var medico = hospital.RegistrarMedico("M101", "Dr. Silva", "Medicina General");
            
            // Asignar paciente a médico
            bool asignacionExitosa = medico.AsignarPaciente(paciente);
            Console.WriteLine($"Asignación exitosa: {asignacionExitosa}");
            
            // Realizar diagnóstico
            bool diagnosticoExitoso = medico.DiagnosticarPaciente(paciente, "Hipertensión arterial");
            Console.WriteLine($"Diagnóstico exitoso: {diagnosticoExitoso}");
            
            // Intentar diagnóstico por personal no autorizado
            var paciente2 = hospital.RegistrarPaciente("H102", "Ana Torres", 32);
            // Este debería fallar si el médico no tiene el paciente asignado
            bool diagnosticoNoAutorizado = medico.DiagnosticarPaciente(paciente2, "Diabetes");
            Console.WriteLine($"Diagnóstico no autorizado (debe ser False): {diagnosticoNoAutorizado}");
            
            Console.WriteLine();
        }

        static void TestSeguridadYAuditoria()
        {
            Console.WriteLine("--- CASO 3: Seguridad ---");
            
            var hospital = new SistemaHospital();
            
            var paciente = hospital.RegistrarPaciente("H201", "Elena Ruiz", 29);
            var medico = hospital.RegistrarMedico("M201", "Dr. Morales", "Neurología");
            
            // Probar acceso a información
            Console.WriteLine("=== Información Básica (Público) ===");
            paciente.MostrarInformacionBasica();
            
            // Suspender licencia médica
            medico.SuspenderLicencia();
            
            // Intentar operaciones con licencia suspendida
            bool asignacionConLicenciaSuspendida = medico.AsignarPaciente(paciente);
            Console.WriteLine($"Asignación con licencia suspendida (debe ser False): {asignacionConLicenciaSuspendida}");
            
            Console.WriteLine();
        }

        static void TestEscenariosLimite()
        {
            Console.WriteLine("--- CASO 4: Escenarios Límite ---");
            
            var hospital = new SistemaHospital();
            var medico = hospital.RegistrarMedico("M301", "Dr. Ocupado", "Medicina General");
            
            // Intentar asignar más de 20 pacientes
            Console.WriteLine("Asignando 22 pacientes (límite es 20):");
            for (int i = 1; i <= 22; i++)
            {
                var paciente = hospital.RegistrarPaciente($"H3{i:D2}", $"Paciente {i}", 25 + i);
                bool asignado = medico.AsignarPaciente(paciente);
                
                if (i <= 20)
                {
                    Console.WriteLine($"Paciente {i}: {(asignado ? "✓ Asignado" : "✗ Error")}");
                }
                else
                {
                    Console.WriteLine($"Paciente {i}: {(asignado ? "✗ No debería asignarse" : "✓ Correctamente rechazado")}");
                }
            }
            
            // Probar con paciente inactivo
            var pacienteInactivo = hospital.RegistrarPaciente("H399", "Paciente Inactivo", 40);
            pacienteInactivo.CambiarEstado("Inactivo");
            
            bool cita = hospital.ProgramarCita(medico, pacienteInactivo, DateTime.Now.AddDays(1));
            Console.WriteLine($"Cita con paciente inactivo (debe ser False): {cita}");
            
            Console.WriteLine();
        }
    }
}