import AsyncStorage from '@react-native-async-storage/async-storage';
export default class {
  static storeData = async (item: any, chiave:string) => {
    const jsonValue = JSON.stringify(item);
    await AsyncStorage.setItem(chiave, jsonValue);
  };
  static getData = async (chiave:string) => {
    const jsonValue = await AsyncStorage.getItem(chiave);
    return jsonValue != null ? JSON.parse(jsonValue) : null;
  };
}
