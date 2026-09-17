async function verificarDisponibilidad(idInmueble, fechaEntrada, fechaSalida) {
  const params = new URLSearchParams({
    idInmueble: idInmueble ?? 0,
    fechaEntrada: fechaEntrada ?? "",
    fechaSalida: fechaSalida ?? "",
  });

  const respuesta = await fetch(`/Reserva/VerificarDisponibilidad?${params}`);

  if (!respuesta.ok) {
    throw new Error(`Error ${respuesta.status}`);
  }

  const data = await respuesta.json();
  return data.disponible;
}

const { createApp } = Vue;

createApp({
  data() {
    return {
      fechaEntrada: document.getElementById("FechaEntrada")?.value ?? "",
      fechaSalida: document.getElementById("FechaSalida")?.value ?? "",
      idInmueble: document.getElementById("IdInmueble")?.value ?? "",
      verificando: false,
      disponible: null,
      debounceTimer: null,
    };
  },
  watch: {
    fechaEntrada() {
      this.programarVerificacion();
    },
    fechaSalida() {
      this.programarVerificacion();
    },
    idInmueble() {
      this.programarVerificacion();
    },
  },
  methods: {
    programarVerificacion() {
      this.disponible = null;
      clearTimeout(this.debounceTimer);
      this.debounceTimer = setTimeout(() => this.verificar(), 400);
    },
    async verificar() {
      if (!this.idInmueble || !this.fechaEntrada || !this.fechaSalida) {
        this.disponible = null;
        return;
      }

      this.verificando = true;
      try {
        this.disponible = await verificarDisponibilidad(
          this.idInmueble,
          this.fechaEntrada,
          this.fechaSalida
        );
      } catch {
        this.disponible = null;
      } finally {
        this.verificando = false;
      }
    },
  },
  mounted() {
    this.verificar();
  },
}).mount("#app-disponibilidad");
