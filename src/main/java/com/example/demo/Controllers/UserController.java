package com.example.demo.Controllers;


import com.example.demo.Entities.Role;
import com.example.demo.Entities.User;
import com.example.demo.Pojos.UserRegistration;
import com.example.demo.Services.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.oauth2.provider.token.TokenStore;
import org.springframework.web.bind.annotation.*;

import java.util.Arrays;
import java.util.List;
import java.util.regex.Pattern;

@RestController
public class UserController {


    @Autowired
    private UserService userService;
    @Autowired
    private TokenStore tokenStore;

    @PostMapping(value = "/register")
    public String register(@RequestBody UserRegistration userRegistration){
        if (!userRegistration.getPassword().equals(userRegistration.getPasswordConfirmation()))
            return "passwords do not match";
        else if (userService.getUser(userRegistration.getUsername())!= null)
            return "user exists";
        //Checking for non alphanumerical characters in the username.
        Pattern pattern = Pattern.compile("[^a-zA-Z0-9]");
        if(pattern.matcher(userRegistration.getUsername()).find())
            return "No special characters are allowed in the username";
        // input sanitization
        userService.save(new User(userRegistration.getUsername(),userRegistration.getPassword(), Arrays.asList(new Role("USER"))));
        return "user created";
    }

    @GetMapping(value = "/users")
    public List<User> users(){
        return userService.getAllUsers();
    }

    @GetMapping(value = "/logouts")
    public void logout(@RequestParam(value = "access_token") String accessToken){

        tokenStore.removeAccessToken(tokenStore.readAccessToken(accessToken));
    }

    @GetMapping(value ="/getUsername")
    public String getUsername(){
        return SecurityContextHolder.getContext().getAuthentication().getName();
    }





}

































