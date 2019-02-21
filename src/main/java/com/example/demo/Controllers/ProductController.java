package com.example.demo.Controllers;

import java.net.URI;
import java.util.List;
import java.util.Optional;

import com.example.demo.Entities.Product;
import com.example.demo.Repositories.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.support.ServletUriComponentsBuilder;



@RestController
public class ProductController {

    @Autowired
    private ProductRepository productRepository;



    @GetMapping("/products/{id}")
    @CrossOrigin(origins = "http://localhost:8080")
    public Product retrieveProduct(@PathVariable long id) {
        Optional<Product> product = productRepository.findById(id);

        return product.get();
    }


    @GetMapping(value = "/products")
    @CrossOrigin(origins = "http://localhost:8080")
    public List<Product> retrieveAllProducts() {
        return productRepository.findAll();
    }

    @DeleteMapping("/products/{id}")
    @CrossOrigin(origins = "http://localhost:8080")
    public void deleteProduct(@PathVariable long id) {
        productRepository.deleteById(id);
    }


    @PostMapping("/product")
    @CrossOrigin(origins = "http://localhost:8080")
    public ResponseEntity<Object> createProduct(@RequestBody Product product) {
        Product savedProduct = productRepository.save(product);

        URI location = ServletUriComponentsBuilder.fromCurrentRequest().path("/{id}")
                .buildAndExpand(savedProduct.getId()).toUri();

        return ResponseEntity.created(location).build();

    }


    @PutMapping("/products/{id}")
    @CrossOrigin(origins = "http://localhost:8080")
    public ResponseEntity<Object> updateProduct(@RequestBody Product product, @PathVariable long id) {

        Optional<Product> productOptional = productRepository.findById(id);

        if (!productOptional.isPresent())
            return ResponseEntity.notFound().build();

        product.setId(id);

        productRepository.save(product);

        return ResponseEntity.noContent().build();
    }




}
