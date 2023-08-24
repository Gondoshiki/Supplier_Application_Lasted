function fnGetUploadDisplay() {
    $('#UploadDisplay').show();
    $('#btnfnUploadPreview').show();
    //========== Hide tb File Preview =======================
    $('#tbFilePreview').fadeOut(2000);
    $('#PreviewFile_Title').fadeOut(2000);
    $('#btnfnResetProcess').hide();
    $('#btnfnEditFile').hide();
    $('#btnfnUploadLocal').hide();
    //========== Hide tb File Revise =======================
    $('#tbFileRevise').hide();
    $('#btnfnEditPreview').hide();
    $('#btnfnCancelEdit').hide();
}

function dataTableController(value) {
    if (value == '1') {
        var table = $('#tbFilePreview').DataTable();
        if (table.column(2).visible() === true) {
            table.column(2).visible(false);

            $('#btnfnEditFile').show();
            $('#btnfnCancelEdit').hide();

            $('#btnfnEditPreview').hide();
            $('#btnfnUploadLocal').toggle();
        }
        else {
            table.column(2).visible(true);

            $('#btnfnUploadLocal').toggle();

            $('#btnfnEditFile').hide();
            $('#btnfnCancelEdit').show();

            $('#btnfnEditPreview').show();

        }
        //if (table.column(2).visible()) table.column(2).visible(false);
        //if (!table.column(2).visible()) table.column(2).visible(true);
        /*$('#btnfnUploadLocal').hide();*/
        //$('#btnfnEditPreview').show();                   
        //$('#btnfnEditFile').hide();
    }
    if (value == '2') {
        var table = $('#tbFileRevise').DataTable();

        if (table.column(2).visible() === true) {
            table.column(2).visible(false);

            /*$('#btnfnUploadRevise').show();*/
            $('#btnfnEditRevise').show();
            $('#btnfnCancelEditRevise').hide();

            $('#btnfnEditPreview').hide();
        }
        else {
            table.column(2).visible(true);



            $('#btnfnCancelEditRevise').show();
            $('#btnfnEditPreview').show();
            $('#btnfnEditRevise').hide();
        }
        //if (table.column(2).visible()) table.column(2).visible(false);
        //if (!table.column(2).visible()) table.column(2).visible(true);
        //$('#btnfnUploadRevise').show();
        //$('#btnfnEditRevise').show();
    }

}
