import React, {useEffect, useState} from 'react';
import Socket from '../classi/Socket';
import {Alert, Button, Text, View} from 'react-native';
import SalvataggioDati from '../classi/SalvataggioDati';
import {useFocusEffect} from '@react-navigation/native';
import { ScreenStackHeaderCenterView } from 'react-native-screens';

export default function HomeScreen({navigation, route}: any) {
  const [chiave, setChiave] = useState(null);
  const [socket, setSocket] = useState<Socket | null>(null);
  const [lastTockenDate, setLastTokenDate] = useState<string>('');
  async function fetchData() {
    setChiave(await SalvataggioDati.getData('@Key'));
    setLastTokenDate(await SalvataggioDati.getData('@Date'));
  }


  useFocusEffect(
    React.useCallback(() => {
      if (chiave === null) fetchData();
      if (socket === null) setSocket(new Socket('ws://192.168.1.239:4500'));
      console.log(socket);
      return () => {
        socket?.Disconnetti();
        console.log('Arrivederci');
        setSocket(null);
      };
    }, []),
  );
  
  useFocusEffect(
    React.useCallback(() => {
      if (route.params?.chiave !== null) {
        setChiave(route.params?.chiave);
        console.log(route.params?.chiave)
        console.log('Key give by the last screen' + chiave);
        setLastTokenDate(route.params?.data);
        console.log('La data che mi sono assegnato è ' + lastTockenDate);
      }
      
    }, [route.params?.chiave, route.params?.data]),
  );

  useEffect(() => {console.log('Le cose sono cambiate');}, [chiave, lastTockenDate, socket]);

  return (
    <View>
      <Button
        color={'red'}
        title="Ho bisogno di un token"
        onPress={() => navigation.navigate('Log-in')}
      />
      <Button
        color={'blue'}
        title="Credo di avere già un token"
        onPress={() => {
          socket?.Manda('OnStart', {username: 'Gianni'});

          socket?.Manda('OnAutoLogin', {codice: chiave});
          let risposto = false;

          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
            }
          }, 5000);

          socket?.Ricevi('OnAutoLoginResponse', risposta => {
            risposto = true;
            if (risposta) {
              navigation.navigate({
                name: 'Main',
                params: {chiave: chiave},
                merge: true,
              });
            } else Alert.alert('Il token non è valido');
          });
        }}
      />
      <Text>Ultimo token ricevuto: {route.params?.data}</Text>
    </View>
  );
}
