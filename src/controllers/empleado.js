import empleado from "../modules/empleado.js";
import bcrypt from "bcryptjs";
import path from "path";
import fs from "fs";
import salida from "../modules/salida.js";
import entrada from "../modules/entrada.js";
import { Op , Sequelize} from "sequelize";
import moment from "moment";

export const listarEmpleado = async (req, res) => {
    const list = await empleado.findAll()
console.log(list.length)
    if(list.length > 0) {
      res.send({success: true,message: 'datos encontrados', data: list})
    }
    else{
     res.status(404).send({success: false, message: 'no data in list', data: []})
     }
    
}


export const buscarEmpleado = async (req,res)=>{
  try {
        const { numeroDocumento } = req.body
        const empleadoEncontrado = await empleado.findOne({where: {numeroDocumento: numeroDocumento }})
        
        if(empleadoEncontrado){
            res.status(200).send({ message: 'user found', user: empleadoEncontrado })
        } else {
            res.status(404).send({ message: 'user not found' })
        }
        
    } catch (error) {
        console.error(error)
        res.status(500).send({ message: 'Error finding user' })
    }
}

export const encontrarEmpleadoNombre = async (req, res) => {
  try {
    const {numeroDocumento} = req.body;

    if (!numeroDocumento) {
      return res.status(400).send({
        success: false,
        message: 'Empleado requerido',
        error: {
          statusCode: 400,
          errorMessage: 'Nombre de empleado es requerido'
        }
      });
    }

    const empledoEncontrados = await empleado.findAll({ where: { numeroDocumento: numeroDocumento
    }, });
    console.log(empledoEncontrados)

    if (empledoEncontrados.length > 0) {
      res.status(200).send({
        success: true,
        message: 'Empleados encontrados',
        data: empledoEncontrados
      });
    } else {
      res.status(404).send({
        success: false,
        message: 'Empleados encontrados',
        error: {
          statusCode: 404,
          errorMessage: 'No se encontró el empleado con el nombre proporcionado'
        }
      });
    }
  } catch (error) {
    console.error('Error encontrando empleado:', error);
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


export const mostrarIngresos = async (req, res) => {
  const { fecha } = req.body;

  // Verificar que la fecha proporcionada tiene el formato esperado yyyy/MM
  const regexFecha = /^\d{4}\/\d{2}$/;
  if (!regexFecha.test(fecha)) {
    return res.status(400).send({
      success: false,
      message: "La fecha proporcionada no tiene el formato esperado (yyyy/MM).",
    });
  }
  
  try {
    // Crear las fechas de inicio y fin para el rango
    const [year, month] = fecha.split('/'); // Desglosar año y mes
    const startDate = new Date(`${year}-${month}-01`); // Primer día del mes
    const endDate = new Date(startDate); // Copiar startDate
    endDate.setMonth(endDate.getMonth() + 1); // Sumar un mes al final
  
    // Consultar empleados con entradas y salidas que coincidan con el patrón de fechas
    const empleados = await empleado.findAll({
      include: [
        {
          model: entrada,
          as: "entradas",
          attributes: ["tipoEntrada", "fechaEntrada", "horaEntrada"],
          where: {
            fechaEntrada: {
              [Op.gte]: startDate, // Mayor o igual que el primer día del mes
              [Op.lt]: endDate, // Menor que el primer día del siguiente mes
            },
          },
        },
        {
          model: salida,
          as: "salidas",
          attributes: ["tipoSalida", "fechaSalida", "horaSalida"],
          where: {
            fechaSalida: {
              [Op.gte]: startDate, // Mayor o igual que el primer día del mes
              [Op.lt]: endDate, // Menor que el primer día del siguiente mes
            },
          },
        },
      ],
    });
  
    // Verificar si se encontraron datos
    if (Array.isArray(empleados) && empleados.length > 0) {
      res.status(200).send({
        success: true,
        message: "Datos encontrados",
        data: empleados,
      });
    } else {
      res.status(404).send({
        success: false,
        message: "No se encontró información",
        error: {
          statusCode: 404,
          errorMessage: "No se encontraron empleados con entradas o salidas para el mes especificado.",
        },
      });
    }
  } catch (error) {
    console.error("Error al obtener horas de empleados:", error);
    res.status(500).send({
      success: false,
      message: "Error en el servidor",
      error: {
        statusCode: 500,
        errorMessage: "Hubo un problema al obtener los datos de los empleados.",
      },
    });
  }
  
};



export const horasEmpleados = async (req, res) => {
  try {
    const {fecha } = req.body;

    const pattern = `%${fecha}%`
    
    // Buscar empleados con las entradas y salidas correspondientes
    const empleadoEncontrado = await empleado.findAll({
      include: [
        {
          model: entrada,
          as: 'entradas', // Alias para la relación con 'entrada'
          attributes: ['idEntrada','fechaEntrada', 'tipoEntrada', 'horaEntrada'],
          required: false,
          where: {
            fechaEntrada: {
              [Op.like]: pattern, // Búsqueda por mes y año
            },
          },
        },
        {
          model: salida,
          as: 'salidas', // Alias para la relación con 'salida'
          attributes: ['idSalida','fechaSalida', 'tipoSalida', 'horaSalida'],
          required: false,
          where: {
            fechaSalida: {
              [Op.like]: pattern, // Búsqueda por mes y año
            },
          },
        },
      ],
    });

    // Verificar si se encontraron empleados
    if (Array.isArray(empleadoEncontrado) && empleadoEncontrado.length > 0) {
      res.status(200).send({
        success: true,
        message: 'Datos encontrados',
        data: empleadoEncontrado,
      });
    } else {
      res.status(404).send({
        success: false,
        message: 'No se encontró información',
        error: {
          statusCode: 400,
          errorMessage: 'No se encontraron empleados con las fechas solicitadas',
        },
      });
    }
  } catch (error) {
    console.error('Error al obtener horas de empleados:', error); // Mejora el mensaje de error
    res.status(500).send({
      success: false,
      message: 'Error en el servidor',
      error: {
        statusCode: 500,
        errorMessage: 'Hubo un problema al obtener los datos de los empleados.',
      },
    });
  }
};

export const generarIngreso = async (req,res)=>{

  try {
    const { numeroDocumento, tipoIngreso} = req.body;

    console.log(numeroDocumento, tipoIngreso)

    const empleadoEncontrado = await empleado.findOne({
      where: { numeroDocumento: numeroDocumento },
    })
    if (empleadoEncontrado) {
      // Verifica el tipo de ingreso
      if (tipoIngreso.includes('entrada')) {
        const crearEntrada = await entrada.create({
          tipoEntrada: tipoIngreso,
          horaEntrada: new Date().toLocaleTimeString('en-US', {
            hour12: false, // Force 24-hour format
            timeZone: 'America/Bogota', // Set time zone to Bogotá, Colombia
          }),
          empleadoIdEmpleado: empleadoEncontrado.idEmpleado,
        })

        res.status(200).send({
          success: true,
          message: 'Entrada registrada correctamente',
        });
      } else if (tipoIngreso.includes('salida')) {
    const crearSalida = await salida.create({
      tipoSalida: tipoIngreso,
      horaSalida: new Date().toLocaleTimeString('en-US', {
        hour12: false, // Force 24-hour format
        timeZone: 'America/Bogota', // Set time zone to Bogotá, Colombia
      }),
      empleadoIdEmpleado: empleadoEncontrado.idEmpleado,
    })
        res.status(200).send({
          success: true,
          message: 'Salida registrada correctamente',
        });
      } else {
        res.status(400).send({
          success: false,
          message: 'Tipo de ingreso no válido',
          
        });
      }
    } else {
      // Empleado no encontrado
      res.status(400).send({
        success: false,
        message: 'Empleado no encontrado',
        
      });
    }
  } catch (error) {
    console.log(error)
    res.status(500).send({
      success: false,
      message: 'Error interno en el servidor',
      
    });
  }
}

export const marcarManualmente = async (req, res) => {
  try {
    const { numeroDocumento, tipoIngreso, fecha, hora } = req.body;

    // Busca el empleado por número de documento
    const empleadoEncontrado = await empleado.findOne({
      where: { numeroDocumento: numeroDocumento },
    });

    if (empleadoEncontrado) {
      // Verifica el tipo de ingreso
      if (tipoIngreso.includes('entrada')) {
        const crearEntrada = await entrada.create({
          tipoEntrada: tipoIngreso,
          fechaEntrada: fecha,
          horaEntrada: hora,
          empleadoIdEmpleado: empleadoEncontrado.idEmpleado,
        })

        res.status(200).send({
          success: true,
          message: 'Entrada registrada correctamente',
        });
      } else if (tipoIngreso.includes('salida')) {
    const crearSalida = await salida.create({
      tipoSalida: tipoIngreso,
      fechaSalida: fecha,
      horaSalida: hora,
      empleadoIdEmpleado: empleadoEncontrado.idEmpleado,
    })
        res.status(200).send({
          success: true,
          message: 'Salida registrada correctamente',
        });
      } else {
        res.status(400).send({
          success: false,
          message: 'Tipo de ingreso no válido',
          
        });
      }
    } else {
      // Empleado no encontrado
      res.status(400).send({
        success: false,
        message: 'Empleado no encontrado',
        
      });
    }
  } catch (error) {
   
    res.status(500).send({
      success: false,
      message: 'Error interno en el servidor',
      
    });
  }
};


export const buscarHorasEmpleado = async (req,res)=>{
  try {
        const { numeroDocumento, fecha } = req.body
        const pattern = `${fecha}%`
        const empleadoEncontrado = await empleado.findAll({
          
          where: { 
            numeroDocumento: numeroDocumento
          },
          include: [
            {
              model: entrada,
              as: 'entradas', // Alias para la relación con 'entrada'
              attributes: ['idEntrada','fechaEntrada', 'tipoEntrada', 'horaEntrada'],
              required: false,
              where: {
                fechaEntrada: {
                  [Op.like]: pattern, // Búsqueda por mes y año
                },
              },
            },
            {
              model: salida,
              as: 'salidas', // Alias para la relación con 'salida'
              attributes: ['idSalida','fechaSalida', 'tipoSalida', 'horaSalida'],
              required: false,
              where: {
                fechaSalida: {
                  [Op.like]: pattern, // Búsqueda por mes y año
                },
              },
            },
          ],

          
          
      });
       
      if (Array.isArray(empleadoEncontrado) && empleadoEncontrado.length > 0) {
            res.status(200).send({success:true,message:'data encontrada', data: empleadoEncontrado})
        } else {
            res.status(404).send({ success: false,
              message: 'No se encontro informacion',
              error: {
                statusCode: 400,
                errorMessage: 'Problema con datos ingresados' }})
        }
        
    } catch (error) {
        console.error(error)
        res.status(500).send({  success: false,
          message: 'Usuario no encontrado',
          error: {
            statusCode: 400,
            errorMessage: 'Problema con datos ingresados' }})
    }
}

export const eliminarEmpleado = async (req, res) => {
  const { idEmpleado } = req.body; // Obtenemos el idUsuario desde el cuerpo de la solicitud

  try {
    // Buscar el usuario con el id proporcionado
    const empleadoEncontrado = await empleado.findOne({
      where: { idEmpleado: idEmpleado },
    });

    // Verificar si el usuario existe
    if (!empleadoEncontrado) {
      return res.status(404).send({ success: false,
        message: 'Empleado no encontrado',
        error: {
          statusCode: 400,
          errorMessage: 'Problema con datos ingresados' }});
    }

    // Eliminar el usuario encontrado
    await empleadoEncontrado.destroy();

    // Responder al cliente si la eliminación fue exitosa
    res.send({ success: true,message: 'usuario eliminado', data: empleadoEncontrado });
  } catch (error) {
    // Manejo de errores en caso de que algo salga mal
    console.log(error);
    res.status(500).send({ success: false,
      message: 'Error al eliminar empleado',
      error: {
        statusCode: 400,
        errorMessage: error.message}});
  }
};

export const crearEmpleado = async (req, res) => {
    try {
      // Recibe los datos del usuario en el body de la solicitud
      const { tipoEmpleado, nombreEmpleado, apellidoEmpleado ,tipoDocumento, numeroDocumento, cargo, RH } = req.body;

  
      // Crea un nuevo usuario en la base de datos
      const nuevoEmpleado = await empleado.create({
        tipoEmpleado,
        nombreEmpleado,
        apellidoEmpleado,
        tipoDocumento,
        numeroDocumento,
        cargo, // Agrega el path de la foto si existe
        RH
       
       
      });
  
      // Envía una respuesta exitosa si el usuario se creó correctamente
      res.send({ success: true,message: 'Empleado creado', data: nuevoEmpleado });
  
    } catch (error) {
      // Envía una respuesta de error en caso de fallo
      console.log(error.message)
      res.status(500).send({ success: false,
        message: 'No se pudo crear empleado',
        error: {
          statusCode: 404,
          errorMessage: 'Problema con datos ingresados'
        } });
    }
  };

  export const editarEmpleado = async (req,res)=>{
    try{
    
    const { idEmpleado ,tipoEmpleado, nombreEmpleado, apellidoEmpleado ,tipoDocumento, numeroDocumento, cargo, RH, estado} = req.body;
    
    const buscarEmpleado = await empleado.findByPk(idEmpleado);
    
    
    if(buscarEmpleado) {
      buscarEmpleado.tipoEmpleado = tipoEmpleado,
      buscarEmpleado.nombreEmpleado = nombreEmpleado,
      buscarEmpleado.apellidoEmpleado = apellidoEmpleado,
      buscarEmpleado.tipoDocumento = tipoDocumento,
      buscarEmpleado.numeroDocumento = numeroDocumento,
      buscarEmpleado.cargo = cargo,
      buscarEmpleado.RH = RH,
      buscarEmpleado.estado = estado
      await buscarEmpleado.save();
      res.send({ success: true,message: 'Empleado editado', data: buscarEmpleado});
 
    }
    else{

      res.send({ success: false,
        message: 'No se pudo editar empleado',
        error: {
          statusCode: 404,
          errorMessage: 'Problema con datos ingresados'
        } });
    }

   }
   catch (error){
console.log(error);
    res.status(500).send({ success: false,
      message: 'No se pudo editar empleado',
      error: {
        statusCode: 404,
        errorMessage: error
      }});
   }

  }

  
  export const inactivarEmpleado = async (req, res) => {
    try {
      const { idEmpleado } = req.body;
      const empleados = await empleado.findByPk(idEmpleado); // Asegurarse que se espera el resultado con await
      
      if (empleados) {
        empleados.estado = false;
        await empleados.save()
        res.status(200).send({ message: 'Empleado inactivado', empleado: empleados })
      }
        
  
        
    } catch (error) {
      console.error('Error al desabilitar el usuario:', error);
      res.status(500).send({ message: 'Error inactive user', error: error.message });
    }
  };

  export const activarEmpleado = async (req, res) => {
    try {
      const { idEmpleado } = req.body;
      
      const empleados = await empleado.findByPk(idEmpleado); // Asegurarse que se espera el resultado con await
      
      if (empleados) {
        empleados.estado = false;
        await empleados.save()
        res.status(200).send({ message: 'Empleado activado', empleado: empleados })
      }
        
  
        
    } catch (error) {
      console.error('Error al activar el usuario:', error);
      res.status(500).send({ message: 'Error active user', error: error.message });
    }
  };
  

  

