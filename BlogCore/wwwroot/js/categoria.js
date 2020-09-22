var dataTable;

//Creamos funcion cargarDatatable
$(document).ready(function () {
    cargarDatatable();
});

//utlizmos la variable que declaramos arriva dataTable y luego mandamos a llamar el id de la Tabla en Index Categoria
function cargarDatatable() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
        "url": "/admin/Categorias/GetAll",  //url de acceso al Area Admin en el controlador categorias con el metodp GetAll 
            "type": "GET", //Tipo de Metodp GET que es traer registro
            "datatype": "json" //Datos del servidor en tipo Json
        },

        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "50%" },
            { "data": "orden", "width": "20%" },

            ///Funcion de los Botones
            {
                "data": "id",
                "render": function (data) {   //  ` ` sintaccis Themplate YG permite leer las versiones jvascrip em  Ymascrip en Delete lleva un evento Onclick por la ventana emergente del pluying para eliminar
                    return `
                    <div class="text-center">  
                    <a href='/Admin/Categorias/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                    <i class='fas fa-edit'></i>Editar
                    </a>
               &nbsp;
                     <a onclick=Delete("/Admin/Categorias/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                    <i class='fas fa-trash-alt'></i>Borrar
                    </a>
               `;
                }, "width": "30%"
                }

        ],
        "language": {
            "emptyTable" :"No hay Registro"
        },
        "width" : "100%"

    });
}

//Funcion Eliminar
function Delete(url) {
    swal({
        title: "Estas seguro de borrar?", //titulo del msm
        text: "Este contenido no se puede recuperar!", //Mensaje en pantalla
        type: "warning", //tipo de sms
        showCancelButton: true, //netado del boton de cancelar
        confirmButtonColor: "#DD6B55", //color del boton de confirmacion
        confirmButtonText: "si, borrar!", // confirmacion del sms
        closeOnconfirm: true
    }, function () {
            $.ajax({
                type: 'DELETE', //metodo llamado Delte 
                url: url, // la url es la misma url que recibe al inicio de la funcion Delete(url)
                success: function (data) { //si se pudo borrar success
                    if (data.success) { // si se genero el borrado
                        toastr.success(data.message); // el pluying toastr con el mensaje
                        dataTable.ajax.reload(); // reload para recargar la tabla y no `paresca el registro

                    }
                    else {
                        toastr.error(data.message)// si no se pudo eliminar

                    }
                }
            });
    });
}