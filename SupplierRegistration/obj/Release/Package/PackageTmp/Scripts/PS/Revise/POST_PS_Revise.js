$(document).ready(function () {

    var UploadFile = $('#Body_UploadFile').val();
    if (UploadFile == 1) {
        $("#UploadBar").show(); $("#cardProcess").hide();
    } else {
        $("#UploadBar").hide(); $("#cardProcess").show();
    }
});
function uploadClick(valid) {
    let searchParams = new URLSearchParams(window.location.search);
    var id = null;
    var id = searchParams.get('id');
    if (Page_ClientValidate(valid)) {
        //var countFlies = null;
        //var countFiles = ReplaceAppRegis.files.length + ReplaceBOJ5.files.length + ReplaceBookBank.files.length + ReplaceOrgCompany.files.length + ReplacePP20.files.length + ReplaceRegisCert.files.length + ReplaceSPS10.files.length
        //$('#hdfFile').val(countFiles);
        swal.fire({
            icon: 'question',
            title: 'Do you want to upload this files ?',
            text: '',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {
            if (result.value) {
                if (valid == "AppRegis") {
                    var filecode = '0';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceAppRegis").click();
                }
                else if (valid == "BOJ5") {
                    var filecode = '1';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceBOJ5").click();
                }
                else if (valid == "BookBank") {
                    var filecode = '2';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceBookBank").click();
                }
                else if (valid == "OrgCompany") {
                    var filecode = '3';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceOrgCompany").click();
                }
                else if (valid == "PP20") {
                    var filecode = '4';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplacePP20").click();
                }
                else if (valid == "RegisCert") {
                    var filecode = '5';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceRegisCert").click();
                }
                else if (valid == "SPS10") {
                    var filecode = '6';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceSPS10").click();
                }
                else if (valid == "SME") {
                    var filecode = '7';
                    $('#Body_hdfFile1').val(filecode);
                    $("#Body_btnReplaceSME").click();
                }
                else {
                    return swal.fire("Error", "Tyr Again.");
                }
                //}then(function (result) {
                //    window.location.href = "PS_Detail.aspx?id=" + id
                //});
                /**/

            }

            //if (result.value) {
            //    $("#Body_btnReplaceAppRegis").click();
            //}
        });
    }
}
