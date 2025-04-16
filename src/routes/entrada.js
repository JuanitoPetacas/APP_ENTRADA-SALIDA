import {listarEntradas, buscarEntrada, generarEntrada, eliminarEntrada} from "../controllers/entrada.js";
import { Router } from "express";


const Entrada = Router();

Entrada.get('/listar/entrada', listarEntradas)
Entrada.post('/buscar/entrada', buscarEntrada)
Entrada.post('/generar/entrada', generarEntrada)
Entrada.post('/eliminar/entrada', eliminarEntrada)

export default Entrada