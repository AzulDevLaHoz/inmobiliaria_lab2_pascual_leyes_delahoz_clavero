$(document).ready(function () {
  $(".select2-ajax").each(function () {
    var $select = $(this);

    var ajaxUrl = $select.data("url");
    var placeholderText = $select.data("placeholder");

    $select.select2({
      theme: "bootstrap-5",
      placeholder: placeholderText,
      minimumInputLength: 2,
      ajax: {
        url: ajaxUrl,
        dataType: "json",
        delay: 300,
        data: function (params) {
          return {
            q: params.term, // El término tipeado
          };
        },
        processResults: function (data) {
          return {
            results: $.map(data, function (item) {
              return {
                id: item.id,
                text: item.texto,
              };
            }),
          };
        },
        cache: true,
      },
    });
  });
});
