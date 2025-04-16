import { listarUsuario, crearUsuario, encontrarUsuario, login, editarUsuario, desactivarUsuario, activarUsuario,encontrarUsuarioNombre, eliminarUsuario  } from "../controllers/usuario.js";
import { Router } from "express";
import usuario from "../modules/usuario.js";
const Usuario = Router()

Usuario.get('/listar/usuario', listarUsuario)
Usuario.post('/buscar/usuario', encontrarUsuario)
Usuario.post('/crear/usuario' ,crearUsuario )
Usuario.post('/login/usuario', login)
Usuario.put('/editar/usuario', editarUsuario)
Usuario.post('/desactivar/usuario', desactivarUsuario)
Usuario.post('/activar/usuario', activarUsuario)
Usuario.post('/buscar/nombre/usuario', encontrarUsuarioNombre)
Usuario.post('/eliminar/usuario', eliminarUsuario)


export default Usuario