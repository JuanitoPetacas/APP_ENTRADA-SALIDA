import empleado from "../modules/empleado.js";
import salida from "../modules/salida.js";



export const  listarSalida = async (req,res)=>{
    const salidas = await salida.findAll({
        include: [{model: empleado, as: 'empleado'}]
    })
    if(salidas.length > 0) {
        res.send({salidas})
      }
      else{
       res.status(200).send({status: 'no data', message: 'no data in list'})
      }
}

export const buscarSalida = async (req, res) => {
    try {
        const {idEmpleado} = req.boy;
    const salidas = await salida.findByPk(idEmpleado, {
        include: [{model: empleado, as: 'empleado'}]
    })

    if(salidas){
        res.status(200).send({message: "entrada encontrada", salidas: salidas})
    }
    else{
        res.status(404).send({message: "entrada no encontrada"})


    }
    } catch (error) {
        res.send({error: error.message})
        
    }
    
} 

export const generarSalida = async (req,res) =>{
    try {
             const {tipoSalida, empleadoIdEmpleado}= req.body;
       
       const salidas = await salida.create({
           tipoSalida,
           empleadoIdEmpleado
       })
       
       const fechaFormateada = new Date(salidas.fechaSalida).toLocaleDateString('es-CO', {
           timeZone: 'America/Bogota',
       });
       
       
       let horaFormateada;
       if (salidas.horaSalida && salidas.horaSalida.val === 'CURRENT_TIME') {
           horaFormateada = new Date().toLocaleTimeString('es-CO', {
               timeZone: 'America/Bogota',
               hour12: false,
               hour: '2-digit',
               minute: '2-digit',
               second: '2-digit'
           });
       } else {
           // Si no es un literal y es una hora válida, puedes usarla
           horaFormateada = new Date(`1970-01-01T${salidas.horaSalida}`).toLocaleTimeString('es-CO', {
               timeZone: 'America/Bogota',
               hour12: false,
               hour: '2-digit',
               minute: '2-digit',
               second: '2-digit'
           });
       }
       
       const empleadoEncontrado = await empleado.findByPk(salidas.empleadoIdEmpleado)
       
       
       res.status(200).send({message: "Salida generada correctamente!", salid: {
           idSalida: salidas.idSalida,
           tipoSalida: salidas.tipoSalida,
           fechaSalida: fechaFormateada,
           horaSalida: horaFormateada,
           empleadoEncontrado
           
       }})
       
    } catch (error) {
        res.status(400).send({error: error.message})
    }
    



}

export const eliminarSalida = async (req,res)=>{

    try {
        const {idSalida} = req.body;
        const salidas = await salida.findByPk(idSalida);
        
        if(salidas){
            await salidas.destroy();
            res.status(200).send({message: "Salida eliminada correctamente"})
        }
        else{
            res.status(404).send({message: "Salida no encontrada"})
        }
    } catch (error) {
        res.status(404).send({message:error});
    }
}