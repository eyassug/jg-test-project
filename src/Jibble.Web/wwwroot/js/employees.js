
function onEmployeesReady() {
    $(document).ready(function () {
        

        var dataTable = $('#tblEmployees').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: true,
                paging: true,
                order: [[1, "asc"]],
                searching: false,
                ajax: {
                    url: 'https://localhost:8001/api/employees'
                },
                columns: [
                    { data: 'id' },
                    { data: 'firstName' },
                    { data: 'lastName' },
                    { data: 'dateOfBirth' }
                ]
        }));
    });
}

function onUploadReady() {
    $('#btnUpload').on('click', function () {
        $.ajax({
            url: 'https://localhost:8001/api/files',
            type: 'POST',

            // Form data
            data: new FormData($('form')[0]),

            // Tell jQuery not to process data or worry about content-type
            // You *must* include these options!
            cache: false,
            contentType: false,
            processData: false,

            // Custom XMLHttpRequest
            xhr: function () {
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) {
                    // For handling the progress of the upload
                    myXhr.upload.addEventListener('progress', function (e) {
                        if (e.lengthComputable) {
                            $('progress').attr({
                                value: e.loaded,
                                max: e.total,
                            });
                        }
                    }, false);
                }
                return myXhr;
            }
        });
    });
}