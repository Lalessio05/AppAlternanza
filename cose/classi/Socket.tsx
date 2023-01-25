export default class Socket {
  socket: WebSocket;
  constructor(indirizzo: string) {
    this.socket = new WebSocket(indirizzo);
    this.socket.onopen = () => {
      console.log('Mi sono connesso');
    };
  }

  Manda(nomeEvento: string, messaggio: any) {
    this.socket.send(
      JSON.stringify({
        nomeEvento: nomeEvento,
        username: messaggio.username,
        password: messaggio.password,
      }),
    );
  }

  Ricevi(nomeEvento: string) {
    this.socket.onmessage = e => {
      let messaggio = e.data.split(':');
      if (messaggio[0] == nomeEvento) {
        console.log(messaggio[1]);
        //ritornalo
      }
    };
  }
}
