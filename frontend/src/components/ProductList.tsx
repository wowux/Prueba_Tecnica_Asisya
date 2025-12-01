import React, { useState, useEffect } from 'react';
import { productService } from '../services/api';

interface Product {
  id: string;
  name: string;
  sku: string;
  price: number;
  categoryName?: string;
  categoryImageUrl?: string;
}

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(false);

  const loadProducts = async () => {
    setLoading(true);
    try {
      const response = await productService.getProducts(page, 20, search);
      setProducts(response.data.products);
      setTotal(response.data.total);
    } catch (error) {
      console.error('Error loading products:', error);
    }
    setLoading(false);
  };

  useEffect(() => {
    loadProducts();
  }, [page, search]);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    setPage(1);
    loadProducts();
  };

  const generateProducts = async () => {
    try {
      await productService.generateProducts(1000);
      loadProducts();
      alert('1000 products generated successfully!');
    } catch (error) {
      alert('Error generating products. Make sure categories exist.');
    }
  };

  return (
    <div style={{ padding: '20px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2>Products ({total})</h2>
        <button onClick={generateProducts} style={{ padding: '10px 20px', backgroundColor: '#28a745', color: 'white', border: 'none' }}>
          Generate 1000 Products
        </button>
      </div>

      <form onSubmit={handleSearch} style={{ marginBottom: '20px' }}>
        <input
          type="text"
          placeholder="Search products..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          style={{ padding: '8px', marginRight: '10px', width: '300px' }}
        />
        <button type="submit" style={{ padding: '8px 16px' }}>Search</button>
      </form>

      {loading ? (
        <div>Loading...</div>
      ) : (
        <>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))', gap: '20px' }}>
            {products.map((product) => (
              <div key={product.id} style={{ border: '1px solid #ddd', padding: '15px', borderRadius: '5px' }}>
                <h3>{product.name}</h3>
                <p><strong>SKU:</strong> {product.sku}</p>
                <p><strong>Price:</strong> ${product.price}</p>
                <p><strong>Category:</strong> {product.categoryName}</p>
                {product.categoryImageUrl && (
                  <img src={product.categoryImageUrl} alt={product.categoryName} style={{ width: '100px', height: '100px', objectFit: 'cover' }} />
                )}
              </div>
            ))}
          </div>

          <div style={{ marginTop: '20px', textAlign: 'center' }}>
            <button
              onClick={() => setPage(p => Math.max(1, p - 1))}
              disabled={page === 1}
              style={{ marginRight: '10px', padding: '8px 16px' }}
            >
              Previous
            </button>
            <span>Page {page} of {Math.ceil(total / 20)}</span>
            <button
              onClick={() => setPage(p => p + 1)}
              disabled={page >= Math.ceil(total / 20)}
              style={{ marginLeft: '10px', padding: '8px 16px' }}
            >
              Next
            </button>
          </div>
        </>
      )}
    </div>
  );
};

export default ProductList;