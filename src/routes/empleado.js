import { listarEmpleado, crearEmpleado, buscarEmpleado, buscarHorasEmpleado, editarEmpleado, inactivarEmpleado, activarEmpleado,encontrarEmpleadoNombre,eliminarEmpleado, horasEmpleados, marcarManualmente, generarIngreso, mostrarIngresos } from "../controllers/empleado.js";
import { Router } from "express";


const Empleado = Router()

Empleado.get('/listar/empleado', listarEmpleado)
Empleado.post('/encontrar/nombre/empleado', encontrarEmpleadoNombre)
Empleado.post('/buscar/empleado', buscarEmpleado)
Empleado.post('/buscar/horas/empleado', buscarHorasEmpleado)
Empleado.post('/crear/empleado', crearEmpleado)
Empleado.put('/editar/empleado', editarEmpleado)
Empleado.post('/inactivar/empleado', inactivarEmpleado)
Empleado.post('/activar/empleado', activarEmpleado)
Empleado.post('/eliminar/empleado', eliminarEmpleado)
Empleado.post('/horas/empleado', horasEmpleados)
Empleado.post('/marcar/manualmente', marcarManualmente)
Empleado.post('/generar/ingreso', generarIngreso)
Empleado.post('/mostrar/ingresos', mostrarIngresos)
export default Empleado;