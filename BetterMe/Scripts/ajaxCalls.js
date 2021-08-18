function ajaxCall(method, api, data, successCB, errorCB) {
    $.ajax({
        type: method,
        url: api,
        data: data,
        cache: false,
        //headers: {
        //    'user-key': '38cc3ca5da22dad1bce5bbbd481d8965'
        //},
        contentType: "application/json",
        dataType: "json",
        success: successCB,
        error: errorCB
    });
}