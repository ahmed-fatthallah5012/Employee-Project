import "./DataTables/jQuery-3.3.1/jquery-3.3.1.min.js";
import './DataTables/DataTables-1.10.24/js/jquery.dataTables.min.js';


export function AddDataTable(table) {    
    $(table).DataTable({
    });    
}


export function DestroyDataTable(table) {    
    $(table).DataTable().destroy();    
}