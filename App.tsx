import React from "react";

import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import LoginScreen from "./screens/LoginScreen";
import MainScreen from "./screens/MainScreen";
import HomeScreen from "./screens/HomeScreen";

const MyStack = createNativeStackNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <MyStack.Navigator>
        <MyStack.Screen name="Home" component={HomeScreen} />
        <MyStack.Screen name="Log-in" component={LoginScreen} />
        <MyStack.Screen name="Main" component={MainScreen} />
        </MyStack.Navigator>
    </NavigationContainer>
  );
};

export default App;