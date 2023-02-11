// calls.js
"use strict";

$(document).ready(() => {

    let $theWarning = $("#theWarning");
    let $logBody = $("#logBody");
    let calls = [];

    $theWarning.hide();
    $logBody.on("click", ".delete-button", function () {
        deleteCall(this);
    });

    var client = new signalR.HubConnectionBuilder().withUrl("/callcenter").build();
    client.on("newCallReceived", newCall => {
        addCall(newCall);
    });

    function addCalls() {
        $logBody.empty();
        $.each(calls, (i, c) => addCall(c));
    }

    function addCall(call) {
        let template = `<tr>
  <td>${call.name}</td>
  <td><button class="btn btn-sm btn-warning delete-button" data-id="${call.id}">Clear</button></td>
</tr>`;
        $logBody.append($(template));
    }

    function deleteCall(button) {
        let id = $(button).attr("data-id");
        $.ajax({
            url: `/api/calls/${id}`,
            method: "delete"
        })
            .then(res => {
                $(button).closest("tr").remove();
            });
    }

    function getCalls() {
        $.getJSON("/api/calls")
            .then(res => {
                calls = res;
                addCalls();
                client.start();
            })
            .catch(() => {
                $theWarning.text("Failed to get calls...");
                $theWarning.show();
            });
    }

    getCalls();
});