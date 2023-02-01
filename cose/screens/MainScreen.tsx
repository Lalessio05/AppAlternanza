import React, {useEffect, useState, useRef} from 'react';
import {View, StyleSheet, Alert, Text, TouchableOpacity} from 'react-native';
import Socket from '../classi/Socket';

export default function MainScreen({navigation, route}: any) {
  const [chiave, setChiave] = useState('');
  const [socket, setSocket] = useState<null | Socket>(null);
  const intervalId = useRef<null | number>(null);

  function HandleOnPressIn(direzione: string) {
    let risposto = false;
    intervalId.current = setInterval(() => {
      socket?.Manda('OnMove', {codice: chiave, movimento: direzione});
      setTimeout(() => {
        if (!risposto) {
          Alert.alert('Il server non risponde');
          return;
        }
      }, 5000);

      socket?.Ricevi('OnMoveResponse', risposta => {
        risposto = true;
        if (!risposta) Alert.alert('Chiave scaduta');
      });
    }, 200);
  }
  useEffect(() => {
    setSocket(new Socket('ws://192.168.1.239:4500'));
    setChiave(route.params?.chiave);
  }, []);

  return (
    <View style={styles.container}>
      <TouchableOpacity
        onPressIn={() => {
          HandleOnPressIn('Sinistra');
        }}
        onPressOut={() => {
          if (intervalId.current !== null) clearInterval(intervalId.current);
        }}
        onPress={() => {
          let risposto: any;
          socket?.Manda('OnMove', {codice: chiave, movimento: 'Sinistra'});
          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
              return;
            }
          }, 5000);

          socket?.Ricevi('OnMoveResponse', risposta => {
            risposto = true;
            if (!risposta) Alert.alert('Chiave scaduta');
          });
        }}>
        <Text style={{color: 'red', fontSize: 30}}> Sinistra</Text>
      </TouchableOpacity>

      <View style={styles.buttonContainer}>
        <TouchableOpacity
          onPressIn={() => {
            HandleOnPressIn('Su');
          }}
          onPressOut={() => {
            if (intervalId.current !== null) clearInterval(intervalId.current);
          }}
          onPress={() => {
            let risposto: any;
            socket?.Manda('OnMove', {codice: chiave, movimento: 'Su'});
            setTimeout(() => {
              if (!risposto) {
                Alert.alert('Il server non risponde');
                return;
              }
            }, 5000);
  
            socket?.Ricevi('OnMoveResponse', risposta => {
              risposto = true;
              if (!risposta) Alert.alert('Chiave scaduta');
            });
          }}>
          <Text style={{color: 'red', fontSize: 30}}> Su</Text>
        </TouchableOpacity>
        <TouchableOpacity
          onPressIn={() => {
            HandleOnPressIn('Giù');
          }}
          onPressOut={() => {
            if (intervalId.current !== null) clearInterval(intervalId.current);
          }}
          onPress={() => {
            let risposto: any;
            socket?.Manda('OnMove', {codice: chiave, movimento: 'Giù'});
            setTimeout(() => {
              if (!risposto) {
                Alert.alert('Il server non risponde');
                return;
              }
            }, 5000);
  
            socket?.Ricevi('OnMoveResponse', risposta => {
              risposto = true;
              if (!risposta) Alert.alert('Chiave scaduta');
            });
          }}
          >
          <Text style={{color: 'red', fontSize: 30}}> Giù</Text>
        </TouchableOpacity>
      </View>
      <TouchableOpacity
        onPressIn={() => {
          HandleOnPressIn('Destra');
        }}
        onPressOut={() => {
          if (intervalId.current !== null) clearInterval(intervalId.current);
        }}
        onPress={() => {
          let risposto: any;
          socket?.Manda('OnMove', {codice: chiave, movimento: 'Destra'});
          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
              return;
            }
          }, 5000);

          socket?.Ricevi('OnMoveResponse', risposta => {
            risposto = true;
            if (!risposta) Alert.alert('Chiave scaduta');
          });
        }}>
        <Text style={{color: 'red', fontSize: 30}}> Destra</Text>
      </TouchableOpacity>
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

//Forse è meglio fare una classe con la funzione che controlla che il server risponda e abbia mandato risposta positiva
//Fare anche l'onPress normale dei pulsanti
//Array di 4 bool da mandare sempre al server
