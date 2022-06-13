
function onEmployeesReady() {
    $(document).ready(function () {
        

        $('#tblEmployees').DataTable({
                dom: 'Bt',
                serverSide: true,
                paging: true,
                order: [[1, "asc"]],
                searching: false,
                ajax: {
                    url: 'https://localhost:8001/api/employees',
                    dataSrc: 'items'
                },
                columns: [
                    { data: 'id' },
                    { data: 'firstName' },
                    { data: 'lastName' },
                    { data: 'dateOfBirth' }
                ]
        });
    });
}

function onUploadReady() {
    $('#btnUpload').on('click', function () {
        $.ajax({
            url: 'https://localhost:8001/api/files',
            type: 'POST',

            // Form data
            data: new FormData($('form')[0]),
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

function onEditReady() {
    $("#xport").click(function (e) {
        export_xlsx();
    });
    var HTMLOUT = document.getElementById('htmlout');
    var xspr = x_spreadsheet(HTMLOUT);
    HTMLOUT.style.height = (window.innerHeight - 400) + "px";
    HTMLOUT.style.width = (window.innerWidth - 50) + "px";

    var exportCsv = (function () {
        function export_xlsx() {
            var wb = xtos(xspr.getData());
            var ws1 = wb.Sheets[wb.SheetNames[0]];
            var csv = XLSX.utils.sheet_to_csv(ws1, { strip: true });
            /* write file and trigger a download */
            XLSX.writeFile(csv, 'edited.csv', {});
        }
    })

    var process_wb = (function () {
        var XPORT = document.getElementById('xport');

        return function process_wb(wb) {
            /* convert to x-spreadsheet form */
            var data = stox(wb);

            /* update x-spreadsheet */
            xspr.loadData(data);
            XPORT.disabled = false;

            if (typeof console !== 'undefined') console.log("output", new Date());
        };
    })();

    var do_file = (function () {
        return function do_file(files) {
            var f = files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                if (typeof console !== 'undefined') console.log("onload", new Date());
                var data = e.target.result;
                data = new Uint8Array(data);
                process_wb(XLSX.read(data, { type: 'array' }));
            };
            reader.readAsArrayBuffer(f);
        };
    })();

    (function () {
        var drop = document.getElementById('drop');
        if (!drop.addEventListener) return;

        function handleDrop(e) {
            e.stopPropagation();
            e.preventDefault();
            do_file(e.dataTransfer.files);
        }

        function handleDragover(e) {
            e.stopPropagation();
            e.preventDefault();
            e.dataTransfer.dropEffect = 'copy';
        }

        drop.addEventListener('dragenter', handleDragover, false);
        drop.addEventListener('dragover', handleDragover, false);
        drop.addEventListener('drop', handleDrop, false);
    })();

    (function () {
        var xlf = document.getElementById('xlf');
        if (!xlf.addEventListener) return;
        function handleFile(e) { do_file(e.target.files); }
        xlf.addEventListener('change', handleFile, false);
    })();

    function export_xlsx() {
        var wb = xtos(xspr.getData());
        var ws1 = wb.Sheets[wb.SheetNames[0]];
        var csv = XLSX.utils.sheet_to_csv(ws1, { strip: true });
        /* write file and trigger a download */
        download_file(csv, 'edited.csv', 'text/csv;encoding:utf-8');
    }

    function download_file(content, fileName, mimeType) {
        var a = document.createElement('a');
        mimeType = mimeType || 'application/octet-stream';

        if (navigator.msSaveBlob) { // IE10
            navigator.msSaveBlob(new Blob([content], {
                type: mimeType
            }), fileName);
        } else if (URL && 'download' in a) { //html5 A[download]
            a.href = URL.createObjectURL(new Blob([content], {
                type: mimeType
            }));
            a.setAttribute('download', fileName);
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        } else {
            location.href = 'data:application/octet-stream,' + encodeURIComponent(content); // only this mime type is supported
        }
    }
}