package com.example.demo.Services;


import com.example.demo.Entities.Product;
import com.example.demo.Repositories.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ProductService {

    @Autowired
    private ProductRepository productRepository;

    public List<Product> getAllProducts(){
        return productRepository.findAll();
    }


    public void insert(Product product) {
        productRepository.save(product);
    }

}