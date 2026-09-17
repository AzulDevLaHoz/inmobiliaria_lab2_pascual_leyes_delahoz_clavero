const BASE_URL = "/Inmueble";

async function buscarDisponibles(filtros) {
  const params = new URLSearchParams({
    fechaEntrada: filtros.fechaEntrada,
    fechaSalida: filtros.fechaSalida,
    capacidad: filtros.capacidad ?? 1,
    idTipoInmueble: filtros.idTipoInmueble ?? 0,
    pagNro: filtros.pagNro ?? 1,
  });

  const respuesta = await fetch(`${BASE_URL}/BuscarDisponibles?${params}`);

  if (!respuesta.ok) {
    const error = await respuesta
      .json()
      .catch(() => ({ error: "Error desconocido" }));
    throw new Error(error.error ?? `Error ${respuesta.status}`);
  }

  return respuesta.json();
}

const { createApp } = Vue;

createApp({
  data() {
    return {
      filtros: {
        fechaEntrada: "",
        fechaSalida: "",
        capacidad: 1,
        idTipoInmueble: 0,
      },
      resultados: [],
      cargando: false,
      error: "",
      buscoAlMenosUnaVez: false,
    };
  },
  methods: {
    async buscar() {
      this.error = "";
      this.cargando = true;
      this.buscoAlMenosUnaVez = true;
      try {
        this.resultados = await buscarDisponibles(this.filtros);
      } catch (e) {
        this.error = e.message;
        this.resultados = [];
      } finally {
        this.cargando = false;
      }
    },
    irADetalle(id) {
      window.location.href = `/Inmueble/Detalles/${id}`;
    },
  },
}).mount("#app");
