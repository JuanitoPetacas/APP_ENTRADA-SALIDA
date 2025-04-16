import usuario from '../modules/usuario.js'
import bcrypt from 'bcryptjs'



export const listarUsuario = async (req, res) => {
    const list = await usuario.findAll()

    if(list.length > 0) {
      res.send({success: true,message: 'datos encontrados', data: list})
    }
    else{
     res.status(200).send({success:false, message: 'no data', error: {statusCode: 404, errorMessage: 'No se encuentra datos encontrados'}})
    }
    
}

export const encontrarUsuario = async (req,res)=>{
  try {
        const { idUsuario } = req.body
        const usuarios  = await usuario.findByPk(idUsuario)
        
        if(usuarios){
            res.status(200).send({ message: 'usuario encontrado', usuario: usuarios })
        } else {
            res.status(404).send({ message: 'usuario no encontrado' })
        }
        
    } catch (error) {
        
        res.status(500).send({ message: 'Error finding user' })
    }
}

export const encontrarUsuarioNombre = async (req, res) => {
  try {
    const { nombreUsuario } = req.body;

    if (!nombreUsuario) {
      return res.status(400).send({
        success: false,
        message: 'Nombre de usuario es requerido',
        error: {
          statusCode: 400,
          errorMessage: 'Nombre de usuario es requerido'
        }
      });
    }

    const usuarios = await usuario.findAll({ where: { nombreUsuario: nombreUsuario } });

    if (usuarios.length > 0) {
      res.status(200).send({
        success: true,
        message: 'Usuario encontrado',
        data: usuarios
      });
    } else {
      res.status(404).send({
        success: false,
        message: 'Usuario no encontrado',
        error: {
          statusCode: 404,
          errorMessage: 'No se encontró el usuario con el nombre proporcionado'
        }
      });
    }
  } catch (error) {
    console.error('Error encontrando usuario:', error);
    res.status(500).send({
      success: false,
      message: 'Error interno del servidor',
      error: {
        statusCode: 500,
        errorMessage: 'Error finding user'
      }
    });
  }
}



export const crearUsuario = async (req, res) => {
    try {
      // Recibe los datos del usuario en el body de la solicitud
      const { tipoUsuario, nombreUsuario, numeroDocumento, passwordUsuario  } = req.body;
  
      // Encripta la contraseña
      const password = encriptarPassword(passwordUsuario);

      // Crea un nuevo usuario en la base de datos
      const nuevoUsuario = await usuario.create({
        tipoUsuario,
        nombreUsuario,
        numeroDocumento,
        passwordUsuario: password
      });
  
      // Envía una respuesta exitosa si el usuario se creó correctamente
      res.send({ success: true,message: 'usuario creado', data: nuevoUsuario });
  
    } catch (error) {
      // Envía una respuesta de error en caso de fallo
      res.status(500).send({success: false,
        message: 'No se pudo crear usuario',
        error: {
          statusCode: 404,
          errorMessage: 'Problema con datos ingresados'
        }});
    }
  };

  export const editarUsuario = async (req,res)=>{
    try{
    
    const { idUsuario ,tipoUsuario, nombreUsuario, numeroDocumento , passwordUsuario, estado} = req.body;
    
   
    const usuarioEditado = await usuario.findByPk(idUsuario);
    
    
    if(usuarioEditado) {
      
        usuarioEditado.tipoUsuario = tipoUsuario,
        usuarioEditado.nombreUsuario = nombreUsuario,
        usuarioEditado.numeroDocumento = numeroDocumento,
        usuarioEditado.passwordUsuario = encriptarPassword(passwordUsuario),
        usuarioEditado.estado = estado
        await usuarioEditado.save();
        res.send({  success: true,message: 'usuario editado correctamente', data: usuarioEditado });
     
 
    }
    else{

       res.status(400).send({
          statusCode: 400,
          errorMessage: 'no se encontro el usuario'
        });
    }

   }
   catch (error){

    res.status(500).send({success: false,
      message: 'Error de tipado de datos',
      error: {
        statusCode: 400,
        errorMessage: 'Problema con datos ingresados'
      }});
   }

  }

  
  export const desactivarUsuario  = async (req, res) => {
    try {
      const { idUsuario } = req.body;
      const usuarios = await usuario.findByPk(idUsuario); // Asegurarse que se espera el resultado con await
      
      if (usuarios) {
        usuarios.estado = 'inactivo'
        await usuarios.save()
        res.status(200).send({ message: 'Usuario inactivo', usuario: usuarios })
      }
        
  
        
    } catch (error) {
      console.error('Error al desabilitar el usuario:', error);
      res.status(500).send({ message: 'Error deleting user', error: error.message });
    }
  };

  export const activarUsuario = async (req, res) => {
    try {
      const { idUsuario } = req.body;
      
      const usuarios = await usuario.findByPk(idUsuario); // Asegurarse que se espera el resultado con await
      
      if (usuarios) {
        usuarios.estado = 'activo'
        await usuarios.save()
        res.status(200).send({ message: 'Usuario activo', usuario: usuarios })
      }
        
  
        
    } catch (error) {
    
      res.status(500).send({ message: 'Error active user', error: error.message });
    }
  };

  export const eliminarUsuario = async (req, res) => {
    const { idUsuario } = req.body; // Obtenemos el idUsuario desde el cuerpo de la solicitud
  
    try {
      // Buscar el usuario con el id proporcionado
      const usuarios = await usuario.findOne({
        where: { idUsuario: idUsuario },
      });
  
      // Verificar si el usuario existe
      if (!usuarios) {
        return res.status(404).send({ success: false,
          message: 'Usuario no encontrado',
          error: {
            statusCode: 400,
            errorMessage: 'Problema con datos ingresados' }});
      }
  
      // Eliminar el usuario encontrado
      await usuarios.destroy();
  
      // Responder al cliente si la eliminación fue exitosa
      res.send({ success: true,message: 'usuario eliminado', data: usuarios });
    } catch (error) {
      // Manejo de errores en caso de que algo salga mal
      console.log(error);
      res.status(500).send({ success: false,
        message: 'Error al eliminar usuario',
        error: {
          statusCode: 400,
          errorMessage: 'Problema con datos ingresados' } });
    }
  };
  

  export const login = async (req,res)=>{
    const {numeroDocumento, passwordUsuario} = req.body
    try {
      const usuarios = await usuario.findOne({ where: { numeroDocumento: numeroDocumento}});
    if(usuarios && bcrypt.compareSync(passwordUsuario, usuarios.passwordUsuario) && usuarios.estado == "ACTIVO"){
      res.send({ message: "Login successful", usuario: usuarios });
    }
    else if(usuarios.estado == "INACTIVO"){
      res.send({message: "el usuario ha sido desactivado"})
    }
    else{
      res.send({message: "incorrect pass or identity, verify your data"})
    }
      
    } catch (error) {
      res.send({"error": error.message})
    }
    
  }



const encriptarPassword = (password) => {
  console.log(password)
  const saltrounds = 10
  const salt = bcrypt.genSaltSync(saltrounds);
  return bcrypt.hashSync(password, salt);
};

