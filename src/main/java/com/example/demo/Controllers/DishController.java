package com.example.demo.Controllers;


import com.example.demo.Config.CustomUserDetails;
import com.example.demo.Entities.Product;
import com.example.demo.Entities.Dish;
import com.example.demo.Repositories.DishRepository;
import com.example.demo.Repositories.ProductRepository;
import com.example.demo.Services.DishService;
import com.example.demo.Services.UserService;
import com.example.demo.Services.ProductService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;

import java.net.URI;
import java.util.List;
import java.util.Optional;

@RestController
public class DishController {

   @Autowired
   private DishRepository dishRepository;


    @GetMapping("/dishes/{id}")
    @CrossOrigin(origins = "http://localhost:8080")
    public Dish retrieveDish(@PathVariable long id) {
        Optional<Dish> dish = dishRepository.findById(id);

        return dish.get();
    }


    @GetMapping(value = "/dishes")
    @CrossOrigin(origins = "http://localhost:8080")
    public List<Dish> retrieveAllDishes() {
        return dishRepository.findAll();
    }

    @DeleteMapping("/dishes/{id}")
    @CrossOrigin(origins = "http://localhost:8080")
    public void deleteDish(@PathVariable long id) {
        dishRepository.deleteById(id);
    }


    @PostMapping("/dishes")
    @CrossOrigin(origins = "http://localhost:8080")
    public ResponseEntity<Object> createDish(@RequestBody Dish dish) {
        Dish savedDish = dishRepository.save(dish);

        URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}")
                .buildAndExpand(savedDish.getId()).toUri();

        return ResponseEntity.created(location).build();

    }


    @PutMapping("/dishes/{id}")
    @CrossOrigin(origins = "http://localhost:8080")
    public ResponseEntity<Object> updateDish(@RequestBody Dish dish, @PathVariable long id) {

        Optional<Dish> dishOptional = dishRepository.findById(id);

        if (!dishOptional.isPresent())
            return ResponseEntity.notFound().build();

        dish.setId(id);

        dishRepository.save(dish);

        return ResponseEntity.noContent().build();
    }













//
//
//
//    @PostMapping(value = "/dish")
//    @CrossOrigin(origins = "http://localhost:8080")
//    public void addDish(@RequestBody Dish dish){
////        CustomUserDetails userDetails = (CustomUserDetails) SecurityContextHolder.getContext().getAuthentication().getPrincipal();
////        product.setCreator(userService.getUser(userDetails.getUsername()));
//        dishService.insert(dish);
//        System.out.println("dish added");
//        System.out.println(dish.toString());
//    }
//
//    @GetMapping(value = "/dishes")
//    @CrossOrigin(origins = "http://localhost:8080")
//    public List<Dish> dishes(){
//        return dishService.getAllDishes();
//    }
//
//












}
