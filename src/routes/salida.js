import {listarSalida, buscarSalida, generarSalida, eliminarSalida} from "../controllers/salida.js";
import {Router} from "express";


const Salida = Router();

Salida.get('/listar/salida', listarSalida)
Salida.post('/buscar/salida', buscarSalida)
Salida.post('/generar/salida', generarSalida)
Salida.post('/eliminar/salida', eliminarSalida)

export default Salida