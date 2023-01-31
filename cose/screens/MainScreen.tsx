import React, {useEffect, useState} from 'react';
import {View, Button, StyleSheet, Alert} from 'react-native';
import Socket from '../classi/Socket';

export default function MainScreen({navigation, route}: any) {
  const [chiave, setChiave] = useState('');
  const [socket, setSocket] = useState<null | Socket>(null);
  useEffect(() => {
    setSocket(new Socket('ws://192.168.1.239:4500'));
    setChiave(route.params?.chiave);
  }, []);

  return (
    <View style={styles.container}>
      <Button
        title="        "
        color={'green'}
        onPress={() => {
          socket?.Manda('OnMove', {codice: chiave, movimento: 'Sinistra'});
          let risposto = false;

          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
            }
          }, 5000);

          socket?.Ricevi('OnMoveResponse', risposta => {
            risposto = true;
            if (!risposta) Alert.alert('Il token non è valido');
          });
        }}
      />
      <View style={styles.buttonContainer}>
        <Button
          title="        "
          color={'green'}
          onPress={() => {
            socket?.Manda('OnMove', {codice: chiave, movimento: 'Su'});
            let risposto = false;

            setTimeout(() => {
              if (!risposto) {
                Alert.alert('Il server non risponde');
              }
            }, 5000);

            socket?.Ricevi('OnMoveResponse', risposta => {
              risposto = true;
              if (!risposta) Alert.alert('Il token non è valido');
            });
          }}
        />
        <Button
          title="        "
          color={'green'}
          onPress={() => {
            socket?.Manda('OnMove', {codice: chiave, movimento: 'Giù'});
            let risposto = false;

            setTimeout(() => {
              if (!risposto) {
                Alert.alert('Il server non risponde');
              }
            }, 5000);

            socket?.Ricevi('OnMoveResponse', risposta => {
              risposto = true;
              if (!risposta) Alert.alert('Il token non è valido');
            });
          }}
        />
      </View>
      <Button
        title="        "
        color={'green'}
        onPress={() => {
          socket?.Manda('OnMove', {codice: chiave, movimento: 'Destra'});
          let risposto = false;

          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
            }
          }, 5000);

          socket?.Ricevi('OnMoveResponse', risposta => {
            risposto = true;
            if (!risposta) Alert.alert('Il token non è valido');
          });
        }}
      />
    </View>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  buttonContainer: {
    flexDirection: 'column',
    justifyContent: 'space-between',
    alignItems: 'center',
    height: '50%',
  },
});

//Aggiungiamo dei bottoni con vari opzioni, timeout, casistiche particolari (), ogni comando coi bottoni deve essere verificato.
//Cercare di fare l'apk, verificare che si salvi il tutto.
//Server offline, quando ricevo qualcosa fai vedere qualcosa
//Fai un omino nel server che va in giro con le frecce, tipo gli indiani
//Forse è meglio fare una classe con la funzione che controlla che il server risponda e abbia mandato risposta positiva