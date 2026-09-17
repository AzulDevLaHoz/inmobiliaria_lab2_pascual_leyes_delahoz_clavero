document.addEventListener("DOMContentLoaded", function () {
  let latInicial = -33.3017; // Ubicacion San Luis
  let lngInicial = -66.3378;

  const inputLat = document.getElementById("inputLat");
  const inputLng = document.getElementById("inputLng");
  const btnGoogleMaps = document.getElementById("btnGoogleMaps");

  if (inputLat.value && inputLng.value) {
    latInicial = parseFloat(inputLat.value.replace(",", "."));
    lngInicial = parseFloat(inputLng.value.replace(",", "."));
    actualizarLinkGoogleMaps(latInicial, lngInicial);
  }

  const mapa = L.map("miMapa").setView([latInicial, lngInicial], 13);

  L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
    maxZoom: 19,
    attribution: "© OpenStreetMap",
  }).addTo(mapa);

  let marcador = L.marker([latInicial, lngInicial]).addTo(mapa);

  mapa.on("click", function (e) {
    const lat = e.latlng.lat;
    const lng = e.latlng.lng;

    marcador.setLatLng([lat, lng]);

    inputLat.value = lat.toFixed(6).replace(".", ",");
    inputLng.value = lng.toFixed(6).replace(".", ",");

    actualizarLinkGoogleMaps(lat, lng);
  });

  function actualizarLinkGoogleMaps(lat, lng) {
    btnGoogleMaps.href = `https://www.google.com/maps/search/?api=1&query=${lat},${lng}`;
    btnGoogleMaps.classList.remove("disabled");
  }
});
