import empleado from "../modules/empleado.js"
import entrada from "../modules/entrada.js"


export const  listarEntradas = async (req,res)=>{
    const entradas = await entrada.findAll({
        include: [{model: empleado, attributes: ['nombreEmpleado', 'apellidoEmpleado', 'numeroDocumento','cargo']}]
    })
    console.log(entradas)
    if(entradas.length > 0) {
        res.send({entradas})
      }
      else{
       res.status(200).send({status: 'no data', message: 'no data in list'})
      }
}

export const buscarEntrada = async (req, res) => {
    try {
        const {empleadoIdEmpleado} = req.body;
        const entradaEncontrada = await entrada.findOne({
            where: { empleadoIdEmpleado }, // Busca por la columna en tu modelo
            include: [{ model: empleado, as: 'empleado' }], // Alias declarado en tu modelo
        });

    if(entradaEncontrada){
        res.status(200).send({message: "entrada encontrada", entrada: entradaEncontrada})
    }
    else{
        res.status(404).send({message: "entrada no encontrada"})


    }
    } catch (error) {
        res.send({error: error.message})
        
    }
    
} 


export const generarEntrada = async (req,res) =>{
    try {
        const {tipoEntrada, empleadoIdEmpleado}= req.body;

const entradas = await entrada.create({
    tipoEntrada,
    empleadoIdEmpleado
})

const fechaFormateada = new Date(entradas.fechaEntrada).toLocaleDateString('es-CO', {
    timeZone: 'America/Bogota',
});


let horaFormateada;
if (entradas.horaEntrada && entradas.horaEntrada.val === 'CURRENT_TIME') {
    horaFormateada = new Date().toLocaleTimeString('es-CO', {
        timeZone: 'America/Bogota',
        hour12: false,
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
    });
} else {
    // Si no es un literal y es una hora válida, puedes usarla
    horaFormateada = new Date(`1970-01-01T${entradas.horaEntrada}`).toLocaleTimeString('es-CO', {
        timeZone: 'America/Bogota',
        hour12: false,
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
    });
}

const empleadoEntrada = await empleado.findByPk(entradas.empleadoIdEmpleado)


res.status(200).send({message: "Entrada generada correctamente!", entrada: {
    idEntrada: entradas.idEntrada,
    tipoEntrada: entradas.tipoEntrada,
    fechaEntrada: fechaFormateada,
    horaEntrada: horaFormateada,
    empleadoEntrada
    
}})

        
    } catch (error) {
        res.status(400).send({error: error.message})
    }
    



}

export const eliminarEntrada = async (req,res)=>{

    try {
        const {idEntrada} = req.body;
        const entradaEncontrada = await entrada.findByPk(idEntrada);
        
        if(entradaEncontrada){
            await entradaEncontrada.destroy();
            res.status(200).send({message: "Entrada eliminada correctamente"})
        }
        else{
            res.status(404).send({message: "Entrada no encontrada"})
        }
    } catch (error) {
        res.status(404).send({message:error});
    }
}