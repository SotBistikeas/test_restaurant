package com.example.demo.Controllers;


import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class SiteController {

    @GetMapping(value="/")
    public String index(){
        return "index";
    }

    @GetMapping(value="/registration")
    public String register(){
        return "register";
    }

    @GetMapping(value="/login")
    public String login(){
        return "login";
    }

    

}
