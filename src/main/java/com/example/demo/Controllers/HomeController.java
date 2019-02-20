package com.example.demo.Controllers;


import com.example.demo.Config.CustomUserDetails;
import com.example.demo.Entities.*;
import com.example.demo.Services.DishService;
import com.example.demo.Services.UserService;
import com.example.demo.Services.ProductService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class HomeController {

    @Autowired
    private ProductService productService;

    @Autowired
    private UserService userService;

    @Autowired
    private DishService dishService;

    @GetMapping(value = "/products")
    @CrossOrigin(origins = "http://localhost:8080")
    public List<Product> products(){
        return productService.getAllProducts();
    }

    @PostMapping(value = "/product")
    @CrossOrigin(origins = "http://localhost:8080")
    public void addProduct(@RequestBody Product product){
//        CustomUserDetails userDetails = (CustomUserDetails) SecurityContextHolder.getContext().getAuthentication().getPrincipal();
//        product.setCreator(userService.getUser(userDetails.getUsername()));
        productService.insert(product);
    }
    @GetMapping(value = "/products/{username}")
    public List<Product> productByUsername(@RequestBody String username){
        return productService.findByUser(userService.getUser(username));
    }
    @PostMapping(value = "/dish")
    @CrossOrigin(origins = "http://localhost:8080")
    public void addDish(@RequestBody Dish dish){
//        CustomUserDetails userDetails = (CustomUserDetails) SecurityContextHolder.getContext().getAuthentication().getPrincipal();
//        product.setCreator(userService.getUser(userDetails.getUsername()));
        dishService.insert(dish);
        System.out.println("dish added");
        System.out.println(dish.toString());
    }

    @GetMapping(value = "/dishes")
    @CrossOrigin(origins = "http://localhost:8080")
    public List<Dish> dishes(){
        return dishService.getAllDishes();
    }














}
