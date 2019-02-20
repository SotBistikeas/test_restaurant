package com.example.demo.Services;


import com.example.demo.Entities.Dish;
import com.example.demo.Entities.User;
import com.example.demo.Repositories.DishRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class DishService {

    @Autowired
    private DishRepository dishRepository;

    public List<Dish> getAllDishes(){
        return dishRepository.findAll();
    }

    public void insert(Dish dish){
        dishRepository.save(dish);
    }



}
